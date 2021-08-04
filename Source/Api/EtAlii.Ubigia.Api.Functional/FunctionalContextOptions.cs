// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Logical;
    using Microsoft.Extensions.Configuration;

    public class FunctionalContextOptions : LogicalContextConfiguration, IFunctionalContextOptions, IEditableFunctionalContextOptions
    {
        public IConfiguration Configuration { get; }
        IFunctionHandlersProvider IEditableFunctionalContextOptions.FunctionHandlersProvider { get => FunctionHandlersProvider ; set => FunctionHandlersProvider = value; }
        public IFunctionHandlersProvider FunctionHandlersProvider { get; private set; }

        IRootHandlerMappersProvider IEditableFunctionalContextOptions.RootHandlerMappersProvider { get => RootHandlerMappersProvider; set => RootHandlerMappersProvider = value; }
        public IRootHandlerMappersProvider RootHandlerMappersProvider { get; private set; }

        ParserOptions IEditableFunctionalContextOptions.ParserOptions { get => ParserOptions; set => ParserOptions = value; }
        public ParserOptions ParserOptions { get; private set; }

        public Func<TraversalProcessorOptions> ProcessorOptionsProvider { get; set; }

        public FunctionalContextOptions(IConfiguration configuration)
        {
            Configuration = configuration;
            FunctionHandlersProvider = EtAlii.Ubigia.Api.Functional.Traversal.FunctionHandlersProvider.Empty;
            RootHandlerMappersProvider = EtAlii.Ubigia.Api.Functional.Traversal.RootHandlerMappersProvider.Empty;
        }
    }
}
