namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Logical;

    public interface ILinqQueryContextConfiguration : IConfiguration<LinqQueryContextConfiguration>
    {
        ILogicalContext LogicalContext { get; }

        LinqQueryContextConfiguration Use(ILogicalContext logicalContext);
    }
}