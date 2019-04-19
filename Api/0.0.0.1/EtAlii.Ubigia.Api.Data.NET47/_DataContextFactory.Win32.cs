//namespace EtAlii.Ubigia.Api.Functional
//[
//    using System
//    using EtAlii.Ubigia.Api.Fabric
//    using EtAlii.Ubigia.Api.Logical
//    using EtAlii.Ubigia.Api.Transport

//    public static class NET47DataContextFactoryExtension
//    [
//        private static readonly Lazy<IFunctionHandlersProvider> _createFunctionHandlersProvider = new Lazy<IFunctionHandlersProvider>(CreateFunctionHandlersProvider)

//        public static IDataContext Create(
//            this DataContextFactory dataContextFactory, 
//            IDataConnection connection, 
//            IDiagnosticsConfiguration diagnostics = null)
//        [
//            var fabricContext = new FabricContextFactory().Create(connection)
//            var logicalContext = new LogicalContextFactory().Create(fabricContext, diagnostics)
//            var configuration = new DataContextConfiguration(logicalContext, _createFunctionHandlersProvider.Value, diagnostics)
//            return dataContextFactory.Create(configuration)
//        }

//        public static IDataContext Create(
//            this DataContextFactory dataContextFactory, 
//            IFabricContext fabricContext, 
//            IDiagnosticsConfiguration diagnostics = null)
//        [
//            var logicalContext = new LogicalContextFactory().Create(fabricContext, diagnostics)
//            var configuration = new DataContextConfiguration(logicalContext, _createFunctionHandlersProvider.Value, diagnostics)
//            return dataContextFactory.Create(configuration)
//        }

//        public static IDataContext Create(
//            this DataContextFactory dataContextFactory, 
//            IFabricContext fabricContext, 
//            IDataContextExtension[] extensions,
//            IDiagnosticsConfiguration diagnostics = null)
//        [
//            var logicalContext = new LogicalContextFactory().Create(fabricContext, diagnostics)
//            var configuration = new DataContextConfiguration(logicalContext, extensions, _createFunctionHandlersProvider.Value, diagnostics)
//            return dataContextFactory.Create(configuration)
//        }

//        private static IFunctionHandlersProvider CreateFunctionHandlersProvider()
//        [
//            var functionHandlers = new IFunctionHandler[]
//            [
//                new FileFunctionHandlerFactory().Create(),
//                new FormatFunctionHandlerFactory().Create(),
//            }
//            return new FunctionHandlersProvider(functionHandlers)
//        }
//    }
//}
