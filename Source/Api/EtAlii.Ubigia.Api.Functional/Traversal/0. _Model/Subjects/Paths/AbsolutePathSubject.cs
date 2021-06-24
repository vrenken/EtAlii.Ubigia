﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public class AbsolutePathSubject : NonRootedPathSubject
    {
        public AbsolutePathSubject(PathSubjectPart part)
        : base(part)
        {
        }

        public AbsolutePathSubject(PathSubjectPart[] parts)
        : base(parts)
        {
        }
    }
}
