namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;

    public interface IGraphSLScriptContextConfiguration : IConfiguration<IGraphSLScriptContextExtension, GraphSLScriptContextConfiguration>
    {
        ILogicalContext LogicalContext { get; }
        IFunctionHandlersProvider FunctionHandlersProvider { get; }
        IRootHandlerMappersProvider RootHandlerMappersProvider { get; }
        IGraphSLScriptContextConfiguration Use(ILogicalContext logicalContext);
        IGraphSLScriptContextConfiguration Use(IFunctionHandlersProvider functionHandlersProvider);
        IGraphSLScriptContextConfiguration Use(IRootHandlerMappersProvider rootHandlerMappersProvider);
        IGraphSLScriptContextConfiguration Use(IDataConnection dataConnection);
    }
}