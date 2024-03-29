﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using Moppet.Lapa;

internal interface ISequencePartParser
{
    LpsParser Parser { get; }
    SequencePart Parse(LpNode node, INodeValidator nodeValidator);
    bool CanParse(LpNode node);
}
