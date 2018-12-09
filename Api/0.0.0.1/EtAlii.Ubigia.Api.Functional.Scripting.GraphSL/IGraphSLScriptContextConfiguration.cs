namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Logical;

    public interface IGraphSLScriptContextConfiguration
    {
        IDataContext DataContext { get; }
        IFunctionHandlersProvider FunctionHandlersProvider { get; }
        IRootHandlerMappersProvider RootHandlerMappersProvider { get; }
        IGraphSLScriptContextConfiguration Use(IDataContext dataContext);
        IGraphSLScriptContextConfiguration Use(IFunctionHandlersProvider functionHandlersProvider);
        IGraphSLScriptContextConfiguration Use(IRootHandlerMappersProvider rootHandlerMappersProvider);
    }
}