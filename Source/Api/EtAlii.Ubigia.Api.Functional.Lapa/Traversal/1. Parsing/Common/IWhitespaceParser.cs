// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal interface IWhitespaceParser
    {
        LpsParser Required { get; }
        LpsParser Optional { get; }
    }
}
