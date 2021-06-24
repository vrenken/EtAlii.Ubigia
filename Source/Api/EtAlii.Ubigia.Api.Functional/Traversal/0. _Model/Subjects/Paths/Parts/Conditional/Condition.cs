// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
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
            return $"({Property} {Type} {(Value != null ? Value.ToString() : "<NULL>")})";
        }
    }
}
