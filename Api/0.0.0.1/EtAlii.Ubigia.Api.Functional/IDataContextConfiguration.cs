namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Logical;

    public interface IDataContextConfiguration
    {
        ILogicalContext LogicalContext { get; }
        IDataContextExtension[] Extensions { get; }

        IFunctionHandlersProvider FunctionHandlersProvider { get; }
        IRootHandlerMappersProvider RootHandlerMappersProvider { get; }
        IDataContextConfiguration Use(IDataContextExtension[] extensions);
        IDataContextConfiguration Use(ILogicalContext logicalContext);
        IDataContextConfiguration Use(IFunctionHandlersProvider functionHandlersProvider);
        IDataContextConfiguration Use(IRootHandlerMappersProvider rootHandlerMappersProvider);
    }
}