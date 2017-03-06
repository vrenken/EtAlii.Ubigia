namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    public class Condition
    {
        public string Property { get; }
        public ConditionType Type { get; }
        public object Value { get; }

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
