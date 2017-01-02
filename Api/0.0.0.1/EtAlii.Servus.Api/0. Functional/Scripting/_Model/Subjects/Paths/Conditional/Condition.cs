namespace EtAlii.Servus.Api.Functional
{
    using System;

    public class Condition
    {
        public string Property { get; private set; }
        public ConditionType Type { get; private set; }
        public object Value { get; private set; }

        public Condition(string property, ConditionType type, object value)
        {
            Property = property;
            Type = type;
            Value = value;
        }

        public override string ToString()
        {
            return String.Format("({0} {1} {2})", Property, Type, Value != null ? Value.ToString() : "<NULL>");
        }
    }
}
