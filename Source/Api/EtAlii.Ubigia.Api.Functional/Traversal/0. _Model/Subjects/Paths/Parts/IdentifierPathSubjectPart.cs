// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public sealed class IdentifierPathSubjectPart : PathSubjectPart
    {
        public Identifier Identifier { get; }

        public IdentifierPathSubjectPart(in Identifier identifier)
        {
            Identifier = identifier;
        }

        public override string ToString()
        {
            return $"&{Identifier}";
        }
    }
}
