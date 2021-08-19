// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    /// <summary>
    /// The UseExtensions class provides methods with which options specific settings can be added without losing the options type.
    /// This comes in very handy during the fluent method chaining involved.
    /// </summary>
    public static class FunctionalOptionsUseExtensions
    {
        public static TFunctionalOptions Use<TFunctionalOptions>(this TFunctionalOptions options, IFunctionHandlersProvider functionHandlersProvider)
            where TFunctionalOptions : FunctionalOptions
        {
            var editableOptions = (IEditableFunctionalOptions) options;
            editableOptions.FunctionHandlersProvider = new FunctionHandlersProvider(functionHandlersProvider.FunctionHandlers, editableOptions.FunctionHandlersProvider.FunctionHandlers);

            return options;
        }

        public static TFunctionalOptions Use<TFunctionalOptions>(this TFunctionalOptions options, IRootHandlerMappersProvider rootHandlerMappersProvider)
            where TFunctionalOptions : FunctionalOptions
        {
            var editableOptions = (IEditableFunctionalOptions) options;
            editableOptions.RootHandlerMappersProvider = rootHandlerMappersProvider;

            return options;
        }

        public static TFunctionalOptions Use<TFunctionalOptions>(this TFunctionalOptions options, FunctionalOptions otherOptions)
            where TFunctionalOptions : FunctionalOptions
        {
            //options.Use((LogicalContextOptions)otherOptions); // This cast is needed!

            var editableOptions = (IEditableFunctionalOptions) options;

            editableOptions.FunctionHandlersProvider = otherOptions.FunctionHandlersProvider;
            editableOptions.RootHandlerMappersProvider = otherOptions.RootHandlerMappersProvider;

            return options;
        }
    }
}
