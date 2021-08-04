// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public interface IEditableFunctionalContextOptions
    {
        IFunctionHandlersProvider FunctionHandlersProvider { get; set; }

        IRootHandlerMappersProvider RootHandlerMappersProvider { get; set; }

        Func<TraversalParserOptions> ParserOptionsProvider { get; set; }
        Func<TraversalProcessorOptions> ProcessorOptionsProvider { get; set; }
    }
}
