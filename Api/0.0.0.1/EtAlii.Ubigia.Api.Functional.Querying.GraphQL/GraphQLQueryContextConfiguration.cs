namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Logical;

    public class GraphQLQueryContextConfiguration : Configuration<IGraphQLQueryContextExtension, GraphQLQueryContextConfiguration>, IGraphQLQueryContextConfiguration
    {
        public ILogicalContext LogicalContext { get; private set; }
        
        public GraphQLQueryContextConfiguration()
        {
        }

        public IGraphQLQueryContextConfiguration Use(ILogicalContext logicalContext)
        {
            LogicalContext = logicalContext;
            return this;
        }
    }
}