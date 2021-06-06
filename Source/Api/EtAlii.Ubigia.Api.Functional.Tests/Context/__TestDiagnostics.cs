// namespace EtAlii.Ubigia.Api.Functional.Context.Tests
// {
//     using EtAlii.xTechnology.Diagnostics;
//
//     internal static class TestDiagnostics
//     {
//         public static IDiagnosticsConfiguration Create()
//         {
//             const string name = "EtAlii";
//             const string category = "EtAlii.Ubigia.Api.Functional";
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
