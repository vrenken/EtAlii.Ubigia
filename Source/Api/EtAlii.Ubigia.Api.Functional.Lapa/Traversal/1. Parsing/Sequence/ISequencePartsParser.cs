// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal interface ISequencePartsParser
    {
        string Id { get; }
        LpsParser Parser { get; }
        SequencePart Parse(LpNode node);
    }
}
