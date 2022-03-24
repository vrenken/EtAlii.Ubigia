// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public sealed class RootSubject : Subject
    {
        public readonly string Name;

        public RootSubject(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"root:{Name}";
        }
    }
}
