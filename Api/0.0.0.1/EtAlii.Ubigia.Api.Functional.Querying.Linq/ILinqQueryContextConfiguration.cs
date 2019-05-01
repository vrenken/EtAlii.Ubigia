namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Logical;

    public interface ILinqQueryContextConfiguration : IConfiguration<ILinqQueryContextExtension, LinqQueryContextConfiguration>
    {
        ILogicalContext LogicalContext { get; }

        ILinqQueryContextConfiguration Use(ILogicalContext logicalContext);
    }
}