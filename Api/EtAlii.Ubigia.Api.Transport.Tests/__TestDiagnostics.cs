// namespace EtAlii.Ubigia.Api.Transport.Tests
// {
//     using EtAlii.xTechnology.Diagnostics;
//
//     internal static class TestDiagnostics
//     {
//         public static IDiagnosticsConfiguration Create()
//         {
//             const string name = "EtAlii";
//             const string category = "EtAlii.Ubigia.Api.Transport";
//
//             var diagnostics = new DiagnosticsFactory().Create(false, false, false,
//                 () => new DisabledLogFactory(),
//                 () => new DisabledProfilerFactory(),
//                 (factory) => factory.Create(name, category),
//                 (factory) => factory.Create(name, category));
//             return diagnostics;
//         }
//     }
// }