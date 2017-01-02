namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.Servus.Api.Logical;

    public interface IDataContextConfiguration
    {
        ILogicalContext LogicalContext { get; }
        IDataContextExtension[] Extensions { get; }

        IFunctionHandlersProvider FunctionHandlersProvider { get; }

        IDataContextConfiguration Use(IDataContextExtension[] extensions);
        IDataContextConfiguration Use(ILogicalContext logicalContext);
        IDataContextConfiguration Use(IFunctionHandlersProvider functionHandlersProvider);
    }
}