namespace EtAlii.Servus.Api.Data
{
    using System;

    public class QueryComponent : PathComponent
    {
        public string Property { get; private set; }
        public string Value { get; private set; }

        public QueryComponent(string property, string value)
        {
            Property = property;
            Value = value;
        }

        public override string ToString()
        {
            return String.Format("{0}:{1}", Property, Value);
        }
    }
}
