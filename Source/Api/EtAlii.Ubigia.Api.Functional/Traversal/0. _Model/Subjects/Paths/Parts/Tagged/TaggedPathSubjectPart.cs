﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public class TaggedPathSubjectPart : PathSubjectPart
    {
        public string Name { get; }
        public string Tag { get; }

        public TaggedPathSubjectPart(string name, string tag)
        {
            Name = name ?? string.Empty;
            Tag = tag ?? string.Empty;
        }

        public override string ToString()
        {
            return $"{Name}#{Tag}";
        }
    }
}
