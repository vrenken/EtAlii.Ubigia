namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    public static class Win32DataContextConfigurationExtension
    {
        public static IFunctionHandlersProvider FunctionHandlersProvider { get { return _functionHandlersProvider.Value; } }
        private static readonly Lazy<IFunctionHandlersProvider> _functionHandlersProvider = new Lazy<IFunctionHandlersProvider>(CreateFunctionHandlersProvider);

        public static IDataContextConfiguration UseWin32(
            this IDataContextConfiguration configuration)
        {
            configuration.Use(FunctionHandlersProvider);
            return configuration;
        }

        private static IFunctionHandlersProvider CreateFunctionHandlersProvider()
        {
            var functionHandlers = new IFunctionHandler[]
            {
                new FileFunctionHandlerFactory().Create(),
                new FormatFunctionHandlerFactory().Create(),
            };
            return new FunctionHandlersProvider(functionHandlers);
        }
    }
}
