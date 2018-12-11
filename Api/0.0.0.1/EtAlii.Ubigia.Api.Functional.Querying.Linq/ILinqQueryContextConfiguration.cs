namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Logical;

    public interface ILinqQueryContextConfiguration
    {
        ILogicalContext LogicalContext { get; }
        ILinqQueryContextExtension[] Extensions { get; }

        ILinqQueryContextConfiguration Use(ILinqQueryContextExtension[] extensions);
        ILinqQueryContextConfiguration Use(ILogicalContext logicalContext);
    }
}