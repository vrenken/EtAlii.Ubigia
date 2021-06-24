// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal interface IConditionParser
    {
        LpsParser Parser { get; }
        string Id { get; }

        Condition Parse(LpNode node);
        bool CanParse(LpNode node);
    }
}
