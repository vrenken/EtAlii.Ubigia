namespace EtAlii.Ubigia.Api.Functional.NET47
{
    public static class DotNet47GraphSLScriptContextConfigurationExtension
    {
        public static GraphSLScriptContextConfiguration UseDotNet47(this GraphSLScriptContextConfiguration configuration)
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
