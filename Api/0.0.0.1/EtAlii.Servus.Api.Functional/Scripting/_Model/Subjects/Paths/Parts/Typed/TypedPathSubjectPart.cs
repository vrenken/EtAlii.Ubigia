﻿namespace EtAlii.Servus.Api.Functional
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
