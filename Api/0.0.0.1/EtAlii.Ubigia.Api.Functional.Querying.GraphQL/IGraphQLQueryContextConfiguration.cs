namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Logical;

    public interface IGraphQLQueryContextConfiguration : IConfiguration<IGraphQLQueryContextExtension, GraphQLQueryContextConfiguration>
    {
        ILogicalContext LogicalContext { get; }

        GraphQLQueryContextConfiguration Use(ILogicalContext logicalContext);
    }
}