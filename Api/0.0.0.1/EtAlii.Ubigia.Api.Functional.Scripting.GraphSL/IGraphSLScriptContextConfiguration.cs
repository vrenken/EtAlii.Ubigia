namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;

    public interface IGraphSLScriptContextConfiguration : IConfiguration<IGraphSLScriptContextExtension, GraphSLScriptContextConfiguration>
    {
        ILogicalContext LogicalContext { get; }
        IFunctionHandlersProvider FunctionHandlersProvider { get; }
        IRootHandlerMappersProvider RootHandlerMappersProvider { get; }
        GraphSLScriptContextConfiguration Use(ILogicalContext logicalContext);
        GraphSLScriptContextConfiguration Use(IFunctionHandlersProvider functionHandlersProvider);
        GraphSLScriptContextConfiguration Use(IRootHandlerMappersProvider rootHandlerMappersProvider);
        GraphSLScriptContextConfiguration Use(IDataConnection dataConnection);
    }
}