namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Logical;

    public interface IDataContextConfiguration
    {
        ILogicalContext LogicalContext { get; }
        IDataContextExtension[] Extensions { get; }

        IDataContextConfiguration Use(IDataContextExtension[] extensions);
        IDataContextConfiguration Use(ILogicalContext logicalContext);
    }
}