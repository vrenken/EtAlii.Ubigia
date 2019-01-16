namespace EtAlii.Ubigia.Api.Functional.NET47
{
    public static class NET47GraphSLScriptContextConfigurationExtension
    {
        public static IGraphSLScriptContextConfiguration UseNET47(this IGraphSLScriptContextConfiguration configuration)
        {
            var functionHandlers = new[] 
            {
                new FileFunctionHandlerFactory().Create(),
                new FormatFunctionHandlerFactory().Create(),
            };
            var functionHandlersProvider = new FunctionHandlersProvider(functionHandlers);

            configuration.Use(functionHandlersProvider);

            return configuration;
        }
    }
}
