// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using Moppet.Lapa;

    internal interface IDateTimeValueParser
    {
        LpsParser Parser { get; }
        string Id { get; }

        DateTime Parse(LpNode node);
        bool CanParse(LpNode node);
    }
}
