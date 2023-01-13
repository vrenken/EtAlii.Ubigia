// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System.Linq;

public sealed class Sequence
{
    public SequencePart[] Parts { get; }

    public Sequence(SequencePart[] parts)
    {
        Parts = parts;
    }

    public override string ToString()
    {
        return string.Concat(Parts.Select(part => part.ToString()));
    }
}
