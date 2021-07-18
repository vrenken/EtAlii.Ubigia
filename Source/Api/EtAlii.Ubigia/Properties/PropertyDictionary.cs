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

        /// <summary>
        /// Create a new, empty PropertyDictionary instance.
        /// </summary>
        public PropertyDictionary()
        {
        }

        /// <summary>
        /// Create a new PropertyDictionary instance given the provided properties.
        /// </summary>
        /// <param name="properties"></param>
        public PropertyDictionary(IPropertyDictionary properties)
            : base(properties)
        {
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
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
