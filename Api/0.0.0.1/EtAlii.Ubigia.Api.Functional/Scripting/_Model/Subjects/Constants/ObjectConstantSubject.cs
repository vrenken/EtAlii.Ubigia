namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;

    internal class ObjectConstantSubject : ConstantSubject
    {
        public readonly IPropertyDictionary Values;

        public ObjectConstantSubject(IPropertyDictionary values)
        {
            Values = values;
        }

        public override string ToString()
        {
            var entries = Values.Select(kvp => String.Format("{0}: {1}", kvp.Key, kvp.Value));
            return String.Join(", ", entries);
        }
    }
}
