// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

    [Serializable]
    public sealed class PropertyDictionary : Dictionary<string, object>, IPropertyDictionary
    {
        public bool Stored { get; internal set; }

        private static readonly char[] _trimChars = {' ', '-'};

        private static readonly IEqualityComparer<object> _valueComparer = EqualityComparer<object>.Default;

        private PropertyDictionary(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public PropertyDictionary()
        {
        }

        public PropertyDictionary(IPropertyDictionary properties)
            : base(properties)
        {
        }

        public int CompareTo(IPropertyDictionary other)
        {

            // TODO: This compareto method has errors.
            var result = 1;

            if (other != null)
            {
                result = 0;
                result += CompareDictionaries(this, other);
            }


            if (result > 0)
            {
                return 1;
            }
            if (result < 0)
            {
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// This method compares the two dictionaries.
        /// It could do with some serious improvement.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        private int CompareDictionaries(IPropertyDictionary first, IPropertyDictionary second)
        {
            var firstKeys = first.Keys.ToArray();
            var secondKeys = second.Keys.ToArray();

            var allKeys = firstKeys
                .Union(secondKeys)
                .ToArray();

            var result = 0;
            foreach (var key in allKeys)
            {
                first.TryGetValue(key, out var firstValue);
                second.TryGetValue(key, out var secondValue);
                if (firstValue is IComparable comparableFirstValue)
                {
                    result += Compare(comparableFirstValue, secondValue);
                }
                else if (secondValue is IComparable comparableSecondValue)
                {
                    result -= Compare(comparableSecondValue, firstValue);
                }
                else
                {
                    result += 0;
                }
            }

            // Let's also check the keys.
            var firstKeysAsString = string.Join("", firstKeys);
            var secondKeysAsString = string.Join("", secondKeys);
            result += string.Compare(firstKeysAsString, secondKeysAsString, StringComparison.Ordinal);

            return result;
        }

        private int Compare(IComparable firstValue, object secondValue)
        {
            var areCompatible = true;
            if (secondValue != null)
            {
                areCompatible = secondValue.GetType() == firstValue.GetType();
            }
            if (areCompatible)
            {
                return firstValue.CompareTo(secondValue);
            }

            // I know, hash codes are not meant for comparison,
            // however there is no other way to get a usable int value from different typed objects.
            // Or is there?
            var firstHashCode = DetermineHashForObject(firstValue);
            var secondHashCode = DetermineHashForObject(secondValue);
            return firstHashCode < secondHashCode ? -1 : 1;
        }

        private int DetermineHashForObject(object value)
        {
            if (value is string valueAsString)
            {
                return DetermineHashForString(valueAsString);
            }
            return value.GetHashCode();
        }

        private int DetermineHashForString(string valueAsString)
        {
            // TODO: Investigate if this is sound.
            var hashCode = int.MinValue;
            var bytes = Encoding.Default.GetBytes(valueAsString);
            foreach (var b in bytes)
            {
                var delta = (int.MaxValue - b) - hashCode;
                if (delta <= 0)
                {
                    hashCode = int.MinValue + -delta;
                }
                hashCode += b;
            }
            return hashCode;
        }

        public bool Equals(IPropertyDictionary other)
        {
            if (Count != other?.Count)
            {
                return false;
            }
            if (Keys.Except(other.Keys).Any())
            {
                return false;
            }
            if (other.Keys.Except(Keys).Any())
            {
                return false;
            }
            foreach (var pair in this)
            {
                var otherValue = other[pair.Key];
                if (!_valueComparer.Equals(pair.Value, otherValue))
                {
                    return false;
                }
            }
            return true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var  kvp in this)
            {
                sb.AppendFormat("{0}: \"{1}\" - ", kvp.Key, kvp.Value);
            }

            return sb.ToString().TrimEnd(_trimChars);
        }
    }
}
