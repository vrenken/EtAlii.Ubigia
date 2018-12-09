namespace EtAlii.Ubigia.Api.Functional
{
    public class GraphSLScriptContextConfiguration : IGraphSLScriptContextConfiguration
    {
        public IDataContext DataContext { get; private set; }
        
        public IFunctionHandlersProvider FunctionHandlersProvider { get; private set; }

        public IRootHandlerMappersProvider RootHandlerMappersProvider { get; private set; }

        public GraphSLScriptContextConfiguration()
        {
            FunctionHandlersProvider = Functional.FunctionHandlersProvider.Empty;
            RootHandlerMappersProvider = Functional.RootHandlerMappersProvider.Empty;
        }

        public IGraphSLScriptContextConfiguration Use(IDataContext dataContext)
        {
            DataContext = dataContext;
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
    }
}