// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal interface IIntegerValueParser
    {
        LpsParser Parser { get; }
        string Id { get; }

        int Parse(LpNode node);
        bool CanParse(LpNode node);
    }
}
