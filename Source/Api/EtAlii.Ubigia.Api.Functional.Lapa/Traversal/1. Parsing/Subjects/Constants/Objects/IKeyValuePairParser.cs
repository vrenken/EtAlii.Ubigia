// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Collections.Generic;
    using Moppet.Lapa;

    internal interface IKeyValuePairParser
    {
        LpsParser Parser { get; }
        string Id { get; }

        KeyValuePair<string, object> Parse(LpNode node);
        bool CanParse(LpNode node);
    }
}
