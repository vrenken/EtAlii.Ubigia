namespace EtAlii.Ubigia.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class PropertyDictionary : Dictionary<string, object>, IPropertyDictionary
    {
        public bool Stored { get { return _stored; } internal set { _stored = value; } }
        private bool _stored;

        private static readonly char[] TrimChars = new char[] {' ', '-'};

        private static readonly IEqualityComparer<object> _valueComparer = EqualityComparer<object>.Default;

        public PropertyDictionary()
            : base()
        {
        }

        public PropertyDictionary(IPropertyDictionary properties)
            : base(properties)
        {
        }

        public int CompareTo(IPropertyDictionary other)
        {

            // TODO: This compareto method has errors. 
            int result = 1;

            if (other != null)
            {
                result = 0;
                result += CompareDictionaries(this, other);
                //result -= CompareDictionaries(other, this);
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
                object firstValue;
                first.TryGetValue(key, out firstValue);
                object secondValue;
                second.TryGetValue(key, out secondValue);
                if (firstValue is IComparable)
                {
                    var areCompatible = true;
                    if (secondValue != null)
                    {
                        areCompatible = secondValue.GetType() == firstValue.GetType();
                    }
                    if (areCompatible)
                    {
                        result += ((IComparable)firstValue).CompareTo(secondValue);
                    }
                    else
                    {
                        // I know, hash codes are not meant for comparison, 
                        // however there is no other way to get a usable int value from different typed objects.
                        // Or is there? 
                        result += firstValue.GetHashCode() < secondValue.GetHashCode() ? -1 : 1;
                    }
                }
                else if (secondValue is IComparable)
                {
                    var areCompatible = true;
                    if (firstValue != null)
                    {
                        areCompatible = firstValue.GetType() == secondValue.GetType();
                    }
                    if (areCompatible)
                    {
                        result -= ((IComparable)secondValue).CompareTo(firstValue);
                    }
                    else
                    {
                        // I know, hash codes are not meant for comparison, 
                        // however there is no other way to get a usable int value from different typed objects.
                        // Or is there? 
                        result -= secondValue.GetHashCode() < firstValue.GetHashCode() ? -1 : 1;
                    }
                }
                else
                {
                    result += 0;
                }
            }

            // Let's also check the keys.
            var firstKeysAsString = String.Join("", firstKeys);
            var secondKeysAsString = String.Join("", secondKeys);
            result += String.Compare(firstKeysAsString, secondKeysAsString, StringComparison.Ordinal);

            return result;
        }

        //public void TryGetValue<TValue>(string key, Action<TValue> assignment)
        //{
        //    object value;
        //    if(TryGetValue(key, out value))
        //    {
        //        assignment((TValue) value);
        //    }
        //}

        public bool Equals(IPropertyDictionary other)
        {
            if (other == null)
            {
                return false;
            }
            if (this.Count != other.Count)
            {
                return false;
            }
            if (this.Keys.Except(other.Keys).Any())
            {
                return false;
            }
            if (other.Keys.Except(this.Keys).Any())
            {
                return false;
            }
            foreach (var pair in this)
            {
                var otherValue = other[pair.Key];
                if (!_valueComparer.Equals(pair.Value, other[pair.Key]))
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

            return sb.ToString().TrimEnd(TrimChars);
        }
    }
}