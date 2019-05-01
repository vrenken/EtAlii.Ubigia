namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;

    public class GraphSLScriptContextConfiguration : Configuration<IGraphSLScriptContextExtension, GraphSLScriptContextConfiguration>, IGraphSLScriptContextConfiguration
    {
        public ILogicalContext LogicalContext { get; private set; }
        
        public IFunctionHandlersProvider FunctionHandlersProvider { get; private set; }

        public IRootHandlerMappersProvider RootHandlerMappersProvider { get; private set; }

        public GraphSLScriptContextConfiguration()
        {
            FunctionHandlersProvider = Functional.FunctionHandlersProvider.Empty;
            RootHandlerMappersProvider = Functional.RootHandlerMappersProvider.Empty;
        }
        
        public IGraphSLScriptContextConfiguration Use(ILogicalContext logicalContext)
        {
            LogicalContext = logicalContext;
            return this;
        }

        public IGraphSLScriptContextConfiguration Use(IFunctionHandlersProvider functionHandlersProvider)
        {
            FunctionHandlersProvider = new FunctionHandlersProvider(functionHandlersProvider.FunctionHandlers, FunctionHandlersProvider.FunctionHandlers);
            return this;
        }

        public IGraphSLScriptContextConfiguration Use(IRootHandlerMappersProvider rootHandlerMappersProvider)
        {
            RootHandlerMappersProvider = rootHandlerMappersProvider;
            return this;
        }
        
        public IGraphSLScriptContextConfiguration Use(IDataConnection dataConnection)
        {
            var fabricContextConfiguration = new FabricContextConfiguration()
                .Use(dataConnection);
            var fabricContext = new FabricContextFactory().Create(fabricContextConfiguration);

            var logicalContextConfiguration = new LogicalContextConfiguration()
                .Use(fabricContext);
            var logicalContext = new LogicalContextFactory().Create(logicalContextConfiguration);

            return Use(logicalContext);
        }

    }
}