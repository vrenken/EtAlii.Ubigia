namespace EtAlii.Ubigia.Api.Functional.Win32
{
    public static class Win32DataContextConfigurationExtension
    {
        public static IDataContextConfiguration UseWin32(this IDataContextConfiguration configuration)
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
