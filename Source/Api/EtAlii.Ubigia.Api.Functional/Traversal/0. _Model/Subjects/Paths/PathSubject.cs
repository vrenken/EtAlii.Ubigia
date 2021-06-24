// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;

    public abstract class PathSubject : Subject
    {
        public PathSubjectPart[] Parts { get; }

        protected PathSubject(PathSubjectPart part)
        {
            Parts = new [] { part };
        }

        protected PathSubject(PathSubjectPart[] parts)
        {
            Parts = parts;
        }

        public override string ToString()
        {
            return string.Concat(Parts.Select(part => part.ToString()));
        }

    }
}
