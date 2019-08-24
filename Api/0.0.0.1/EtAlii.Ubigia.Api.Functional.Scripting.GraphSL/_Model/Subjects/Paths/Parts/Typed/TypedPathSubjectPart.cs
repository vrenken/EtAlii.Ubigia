﻿namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    public class TypedPathSubjectPart : PathSubjectPart
    {
        public TypedPathFormatter Formatter { get; }

        public string Type { get; }

        public TypedPathSubjectPart(TypedPathFormatter formatter)
        {
            Formatter = formatter;
            Type = formatter.Type;
        }

        public override string ToString()
        {
            return $"[{Type}]";
        }
    }
}
