namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Logical;

    public interface IGraphQLQueryContextConfiguration : IConfiguration<IGraphQLQueryContextExtension, GraphQLQueryContextConfiguration>
    {
        ILogicalContext LogicalContext { get; }

        IGraphQLQueryContextConfiguration Use(ILogicalContext logicalContext);
    }
}