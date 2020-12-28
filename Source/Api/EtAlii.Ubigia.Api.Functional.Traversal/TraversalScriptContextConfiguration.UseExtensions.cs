namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Logical;

    /// <summary>
    /// The UseExtensions class provides methods with which configuration specific settings can be configured without losing configuration type.
    /// This comes in very handy during the fluent method chaining involved.
    /// </summary>
    public static class TraversalScriptContextConfigurationUseExtensions
    {
        public static TTraversalScriptContextConfiguration Use<TTraversalScriptContextConfiguration>(this TTraversalScriptContextConfiguration configuration, IFunctionHandlersProvider functionHandlersProvider)
            where TTraversalScriptContextConfiguration : TraversalScriptContextConfiguration
        {
            var editableConfiguration = (IEditableTraversalScriptContextConfiguration) configuration;
            editableConfiguration.FunctionHandlersProvider = new FunctionHandlersProvider(functionHandlersProvider.FunctionHandlers, editableConfiguration.FunctionHandlersProvider.FunctionHandlers);

            return configuration;
        }

        public static TTraversalScriptContextConfiguration Use<TTraversalScriptContextConfiguration>(this TTraversalScriptContextConfiguration configuration, IRootHandlerMappersProvider rootHandlerMappersProvider)
            where TTraversalScriptContextConfiguration : TraversalScriptContextConfiguration
        {
            var editableConfiguration = (IEditableTraversalScriptContextConfiguration) configuration;
            editableConfiguration.RootHandlerMappersProvider = rootHandlerMappersProvider;

            return configuration;
        }

        public static TTraversalScriptContextConfiguration Use<TTraversalScriptContextConfiguration>(this TTraversalScriptContextConfiguration configuration, TraversalScriptContextConfiguration otherConfiguration)
            where TTraversalScriptContextConfiguration : TraversalScriptContextConfiguration
        {
            // ReSharper disable once RedundantCast
            configuration.Use((LogicalContextConfiguration)otherConfiguration); // This cast is needed!

            var editableConfiguration = (IEditableTraversalScriptContextConfiguration) configuration;

            editableConfiguration.FunctionHandlersProvider = otherConfiguration.FunctionHandlersProvider;
            editableConfiguration.RootHandlerMappersProvider = otherConfiguration.RootHandlerMappersProvider;

            return configuration;
        }
    }
}