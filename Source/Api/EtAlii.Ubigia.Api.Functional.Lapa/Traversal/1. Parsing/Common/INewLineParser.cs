// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using Moppet.Lapa;

internal interface INewLineParser
{
    LpsChain Required { get; }
    LpsChain Optional { get; }
    LpsParser OptionalMultiple { get; }
}
