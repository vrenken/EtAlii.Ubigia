// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;

    public class ValuePrefix
    {
        public ValueType ValueType { get; }
        public Requirement Requirement { get; }

        public ValuePrefix(ValueType valueType, Requirement requirement)
        {
            ValueType = valueType;
            Requirement = requirement;
        }

        public override string ToString()
        {
            var requirementAsString = Requirement switch
            {
                Requirement.None => "",
                Requirement.Mandatory => "!",
                Requirement.Optional => "?",
                _ => throw new NotSupportedException()
            };
            var typeAsString = ValueType switch
            {
                ValueType.Object => "object",
                ValueType.String => "string",
                ValueType.Bool => "bool",
                ValueType.Float => "float",
                ValueType.Int => "int",
                ValueType.DateTime => "datetime",
                _ => throw new NotSupportedException()
            };
            return $"{typeAsString}{requirementAsString}";
        }
    }
}
