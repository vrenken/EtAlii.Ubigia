﻿namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;

    public class ObjectConstantSubject : ConstantSubject
    {
        public IPropertyDictionary Values => _values;
        private readonly IPropertyDictionary _values;

        public ObjectConstantSubject(IPropertyDictionary values)
        {
            _values = values;
        }

        public override string ToString()
        {
            var entries = _values.Select(kvp => $"{kvp.Key}: {kvp.Value}");
            return string.Join(", ", entries);
        }
    }
}
