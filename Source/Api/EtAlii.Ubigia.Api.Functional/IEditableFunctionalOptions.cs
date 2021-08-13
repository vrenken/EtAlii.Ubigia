// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public interface IEditableFunctionalOptions
    {
        IFunctionHandlersProvider FunctionHandlersProvider { get; set; }

        IRootHandlerMappersProvider RootHandlerMappersProvider { get; set; }
    }
}
