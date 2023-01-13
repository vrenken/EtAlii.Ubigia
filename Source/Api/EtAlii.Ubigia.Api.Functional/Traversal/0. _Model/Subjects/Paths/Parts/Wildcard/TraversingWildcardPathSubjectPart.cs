// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

public sealed class TraversingWildcardPathSubjectPart : PathSubjectPart
{
    public int Limit { get; }

    public TraversingWildcardPathSubjectPart(int limit)
    {
        Limit = limit;
    }

    public override string ToString()
    {
        return Limit == 0 ? "**" : $"*{Limit}*";
    }
}
