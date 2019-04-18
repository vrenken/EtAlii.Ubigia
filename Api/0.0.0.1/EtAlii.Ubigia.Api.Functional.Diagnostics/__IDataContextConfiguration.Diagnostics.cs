//namespace EtAlii.Ubigia.Api.Functional.Diagnostics
//{
//    using EtAlii.xTechnology.Diagnostics
//
//    public static class DataContextConfigurationDiagnosticsExtension 
//    {
//        public static IDataContextConfiguration Use(this IDataContextConfiguration configuration, IDiagnosticsConfiguration diagnostics)
//        {
//            var extensions = new IDataContextExtension[]
//            {
//                new DiagnosticsDataContextExtension(diagnostics)
//            };
//            return configuration.Use(extensions)
//        }
//    }
//}