// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;

    public class RootedPathSubject : PathSubject
    {
        public string Root { get; }

        public RootedPathSubject(string root, PathSubjectPart part)
            : base(part)
        {
            Root = root;
        }

        public RootedPathSubject(string root, PathSubjectPart[] parts)
            : base(parts)
        {
            Root = root;
        }

        public override string ToString()
        {
            return Root + ":" + string.Concat(Parts.Select(part => part.ToString()));
        }

    }
}
