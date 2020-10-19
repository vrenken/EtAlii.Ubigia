//namespace EtAlii.Ubigia.Api.Functional.NET47
//{
//    public static class DotNet47GraphSLScriptContextConfigurationExtension
//    {
//        public static TGraphSLScriptContextConfiguration UseDotNet47<TGraphSLScriptContextConfiguration>(this TGraphSLScriptContextConfiguration configuration)
//            where TGraphSLScriptContextConfiguration : GraphSLScriptContextConfiguration
//        {
//            var functionHandlers = new[] 
//            {
//                new FileFunctionHandlerFactory().Create(),
//                new FormatFunctionHandlerFactory().Create(),
//            };
//            var functionHandlersProvider = new FunctionHandlersProvider(functionHandlers);
//
//            configuration.Use(functionHandlersProvider);
//
//            return configuration;
//        }
//    }
//}
