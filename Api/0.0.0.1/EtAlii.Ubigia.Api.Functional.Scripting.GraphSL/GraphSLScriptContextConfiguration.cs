namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;

    public class GraphSLScriptContextConfiguration : Configuration, IGraphSLScriptContextConfiguration
    {
        public ILogicalContext LogicalContext { get; private set; }
        
        public IFunctionHandlersProvider FunctionHandlersProvider { get; private set; }

        public IRootHandlerMappersProvider RootHandlerMappersProvider { get; private set; }

        public GraphSLScriptContextConfiguration()
        {
            FunctionHandlersProvider = Functional.FunctionHandlersProvider.Empty;
            RootHandlerMappersProvider = Functional.RootHandlerMappersProvider.Empty;
        }
        
        public GraphSLScriptContextConfiguration Use(ILogicalContext logicalContext)
        {
            LogicalContext = logicalContext;
            return this;
        }

        public GraphSLScriptContextConfiguration Use(IFunctionHandlersProvider functionHandlersProvider)
        {
            FunctionHandlersProvider = new FunctionHandlersProvider(functionHandlersProvider.FunctionHandlers, FunctionHandlersProvider.FunctionHandlers);
            return this;
        }

        public GraphSLScriptContextConfiguration Use(IRootHandlerMappersProvider rootHandlerMappersProvider)
        {
            RootHandlerMappersProvider = rootHandlerMappersProvider;
            return this;
        }
        
        public GraphSLScriptContextConfiguration Use(IDataConnection dataConnection)
        {
            var configuration = new LogicalContextConfiguration()
                    .Use(dataConnection);
            
            var logicalContext = new LogicalContextFactory().Create(configuration);

            return Use(logicalContext);
        }

    }
}