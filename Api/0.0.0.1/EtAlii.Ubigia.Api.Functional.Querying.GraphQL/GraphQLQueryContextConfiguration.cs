namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Logical;

    public class GraphQLQueryContextConfiguration : Configuration<IGraphQLQueryContextExtension, GraphQLQueryContextConfiguration>, IGraphQLQueryContextConfiguration
    {
        public ILogicalContext LogicalContext { get; private set; }
        
        public GraphQLQueryContextConfiguration()
        {
        }

        public GraphQLQueryContextConfiguration Use(ILogicalContext logicalContext)
        {
            LogicalContext = logicalContext;
            return this;
        }
    }
}