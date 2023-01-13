// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

public abstract class NonRootedPathSubject : PathSubject
{
    protected NonRootedPathSubject(PathSubjectPart part)
        : base(part)
    {
    }

    protected NonRootedPathSubject(PathSubjectPart[] parts)
        : base(parts)
    {
    }
}
