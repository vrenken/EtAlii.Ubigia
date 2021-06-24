// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public class RegexPathSubjectPart : PathSubjectPart
    {
        public string Regex { get; }

        public RegexPathSubjectPart(string regex)
        {
            Regex = regex;
        }

        public override string ToString()
        {
            return Regex;
        }
    }
}
