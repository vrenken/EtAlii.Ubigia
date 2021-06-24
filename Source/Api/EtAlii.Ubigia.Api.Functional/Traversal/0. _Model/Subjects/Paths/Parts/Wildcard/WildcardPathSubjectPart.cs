// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public class WildcardPathSubjectPart : PathSubjectPart
    {
        public string Pattern { get; }

        public WildcardPathSubjectPart(string pattern)
        {
            Pattern = pattern;
        }

        public override string ToString()
        {
            return Pattern;
        }
    }
}
