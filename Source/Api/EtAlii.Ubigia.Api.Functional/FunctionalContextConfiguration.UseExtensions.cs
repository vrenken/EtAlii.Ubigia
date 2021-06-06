namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Logical;

    /// <summary>
    /// The UseExtensions class provides methods with which configuration specific settings can be configured without losing configuration type.
    /// This comes in very handy during the fluent method chaining involved.
    /// </summary>
    public static class FunctionalContextConfigurationUseExtensions
    {
        public static TFunctionalContextConfiguration Use<TFunctionalContextConfiguration>(this TFunctionalContextConfiguration configuration, IFunctionHandlersProvider functionHandlersProvider)
            where TFunctionalContextConfiguration : FunctionalContextConfiguration
        {
            var editableConfiguration = (IEditableFunctionalContextConfiguration) configuration;
            editableConfiguration.FunctionHandlersProvider = new FunctionHandlersProvider(functionHandlersProvider.FunctionHandlers, editableConfiguration.FunctionHandlersProvider.FunctionHandlers);

            return configuration;
        }

        public static TFunctionalContextConfiguration Use<TFunctionalContextConfiguration>(this TFunctionalContextConfiguration configuration, IRootHandlerMappersProvider rootHandlerMappersProvider)
            where TFunctionalContextConfiguration : FunctionalContextConfiguration
        {
            var editableConfiguration = (IEditableFunctionalContextConfiguration) configuration;
            editableConfiguration.RootHandlerMappersProvider = rootHandlerMappersProvider;

            return configuration;
        }

        public static TFunctionalContextConfiguration Use<TFunctionalContextConfiguration>(this TFunctionalContextConfiguration configuration, FunctionalContextConfiguration otherConfiguration)
            where TFunctionalContextConfiguration : FunctionalContextConfiguration
        {
            // ReSharper disable once RedundantCast
            configuration.Use((LogicalContextConfiguration)otherConfiguration); // This cast is needed!

            var editableConfiguration = (IEditableFunctionalContextConfiguration) configuration;

            editableConfiguration.FunctionHandlersProvider = otherConfiguration.FunctionHandlersProvider;
            editableConfiguration.RootHandlerMappersProvider = otherConfiguration.RootHandlerMappersProvider;
            editableConfiguration.ParserConfigurationProvider = otherConfiguration.ParserConfigurationProvider;
            editableConfiguration.ProcessorConfigurationProvider = otherConfiguration.ProcessorConfigurationProvider;

            return configuration;
        }
    }
}
