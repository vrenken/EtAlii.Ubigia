// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal interface IFunctionSubjectArgumentsParser
    {
        string Id { get; }
        LpsParser Parser { get; }
        FunctionSubjectArgument Parse(LpNode node);
    }
}
