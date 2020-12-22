namespace EtAlii.Ubigia.Api.Functional.Traversal
{
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
            var entries = Values.Select(kvp => $"{kvp.Key}: {kvp.Value}");
            return string.Join(", ", entries);
        }
    }
}
