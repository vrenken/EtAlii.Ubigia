// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public sealed class TypedPathSubjectPart : PathSubjectPart
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
