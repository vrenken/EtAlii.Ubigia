namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Logical;

    /// <summary>
    /// The UseExtensions class provides methods with which configuration specific settings can be configured without losing configuration type.
    /// This comes in very handy during the fluent method chaining involved.
    /// </summary>
    public static class TraversalContextConfigurationUseExtensions
    {
        public static TTraversalContextConfiguration Use<TTraversalContextConfiguration>(this TTraversalContextConfiguration configuration, IFunctionHandlersProvider functionHandlersProvider)
            where TTraversalContextConfiguration : TraversalContextConfiguration
        {
            var editableConfiguration = (IEditableTraversalContextConfiguration) configuration;
            editableConfiguration.FunctionHandlersProvider = new FunctionHandlersProvider(functionHandlersProvider.FunctionHandlers, editableConfiguration.FunctionHandlersProvider.FunctionHandlers);

            return configuration;
        }

        public static TTraversalContextConfiguration Use<TTraversalContextConfiguration>(this TTraversalContextConfiguration configuration, IRootHandlerMappersProvider rootHandlerMappersProvider)
            where TTraversalContextConfiguration : TraversalContextConfiguration
        {
            var editableConfiguration = (IEditableTraversalContextConfiguration) configuration;
            editableConfiguration.RootHandlerMappersProvider = rootHandlerMappersProvider;

            return configuration;
        }

        public static TTraversalContextConfiguration Use<TTraversalContextConfiguration>(this TTraversalContextConfiguration configuration, TraversalContextConfiguration otherConfiguration)
            where TTraversalContextConfiguration : TraversalContextConfiguration
        {
            // ReSharper disable once RedundantCast
            configuration.Use((LogicalContextConfiguration)otherConfiguration); // This cast is needed!

            var editableConfiguration = (IEditableTraversalContextConfiguration) configuration;

            editableConfiguration.FunctionHandlersProvider = otherConfiguration.FunctionHandlersProvider;
            editableConfiguration.RootHandlerMappersProvider = otherConfiguration.RootHandlerMappersProvider;
            editableConfiguration.ParserConfiguration = otherConfiguration.ParserConfiguration;

            return configuration;
        }
    }
}
