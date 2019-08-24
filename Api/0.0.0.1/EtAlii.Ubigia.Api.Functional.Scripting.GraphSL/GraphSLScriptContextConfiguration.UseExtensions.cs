namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using EtAlii.Ubigia.Api.Logical;

    /// <summary>
    /// The UseExtensions class provides methods with which configuration specific settings can be configured without losing configuration type.
    /// This comes in very handy during the fluent method chaining involved. 
    /// </summary>
    public static class GraphSLScriptContextConfigurationUseExtensions
    {
        public static TGraphSLScriptContextConfiguration Use<TGraphSLScriptContextConfiguration>(this TGraphSLScriptContextConfiguration configuration, IFunctionHandlersProvider functionHandlersProvider)
            where TGraphSLScriptContextConfiguration : GraphSLScriptContextConfiguration
        {
            var editableConfiguration = (IEditableGraphSLScriptContextConfiguration) configuration;
            editableConfiguration.FunctionHandlersProvider = new FunctionHandlersProvider(functionHandlersProvider.FunctionHandlers, editableConfiguration.FunctionHandlersProvider.FunctionHandlers);

            return configuration;
        }

        public static TGraphSLScriptContextConfiguration Use<TGraphSLScriptContextConfiguration>(this TGraphSLScriptContextConfiguration configuration, IRootHandlerMappersProvider rootHandlerMappersProvider)
            where TGraphSLScriptContextConfiguration : GraphSLScriptContextConfiguration
        {
            var editableConfiguration = (IEditableGraphSLScriptContextConfiguration) configuration;
            editableConfiguration.RootHandlerMappersProvider = rootHandlerMappersProvider;
            
            return configuration;
        }

        public static TGraphSLScriptContextConfiguration Use<TGraphSLScriptContextConfiguration>(this TGraphSLScriptContextConfiguration configuration, GraphSLScriptContextConfiguration otherConfiguration)
            where TGraphSLScriptContextConfiguration : GraphSLScriptContextConfiguration
        {
            configuration.Use((LogicalContextConfiguration)otherConfiguration);
            
            var editableConfiguration = (IEditableGraphSLScriptContextConfiguration) configuration;

            editableConfiguration.FunctionHandlersProvider = otherConfiguration.FunctionHandlersProvider;
            editableConfiguration.RootHandlerMappersProvider = otherConfiguration.RootHandlerMappersProvider;

            return configuration;
        }
    }
}