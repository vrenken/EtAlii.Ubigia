// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Logical;

    /// <summary>
    /// The UseExtensions class provides methods with which options specific settings can be added without losing the options type.
    /// This comes in very handy during the fluent method chaining involved.
    /// </summary>
    public static class FunctionalContextOptionsUseExtensions
    {
        public static TFunctionalContextOptions Use<TFunctionalContextOptions>(this TFunctionalContextOptions options, IFunctionHandlersProvider functionHandlersProvider)
            where TFunctionalContextOptions : FunctionalContextOptions
        {
            var editableOptions = (IEditableFunctionalContextOptions) options;
            editableOptions.FunctionHandlersProvider = new FunctionHandlersProvider(functionHandlersProvider.FunctionHandlers, editableOptions.FunctionHandlersProvider.FunctionHandlers);

            return options;
        }

        public static TFunctionalContextOptions Use<TFunctionalContextOptions>(this TFunctionalContextOptions options, IRootHandlerMappersProvider rootHandlerMappersProvider)
            where TFunctionalContextOptions : FunctionalContextOptions
        {
            var editableOptions = (IEditableFunctionalContextOptions) options;
            editableOptions.RootHandlerMappersProvider = rootHandlerMappersProvider;

            return options;
        }

        public static TFunctionalContextOptions Use<TFunctionalContextOptions>(this TFunctionalContextOptions options, FunctionalContextOptions otherOptions)
            where TFunctionalContextOptions : FunctionalContextOptions
        {
            // ReSharper disable once RedundantCast
            options.Use((LogicalContextConfiguration)otherOptions); // This cast is needed!

            var editableOptions = (IEditableFunctionalContextOptions) options;

            editableOptions.FunctionHandlersProvider = otherOptions.FunctionHandlersProvider;
            editableOptions.RootHandlerMappersProvider = otherOptions.RootHandlerMappersProvider;
            editableOptions.ParserOptionsProvider = otherOptions.ParserOptionsProvider;
            editableOptions.ProcessorOptionsProvider = otherOptions.ProcessorOptionsProvider;

            return options;
        }
    }
}
