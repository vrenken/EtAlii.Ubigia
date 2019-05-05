namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Logical;

    public interface IGraphQLQueryContextConfiguration : IConfiguration
    {
        ILogicalContext LogicalContext { get; }

        GraphQLQueryContextConfiguration Use(ILogicalContext logicalContext);
    }
}