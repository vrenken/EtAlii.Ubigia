namespace EtAlii.Ubigia.Api.Functional.NET47
{
    public static class NET47DataContextConfigurationExtension
    {
        public static IDataContextConfiguration UseNET47(this IDataContextConfiguration configuration)
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
