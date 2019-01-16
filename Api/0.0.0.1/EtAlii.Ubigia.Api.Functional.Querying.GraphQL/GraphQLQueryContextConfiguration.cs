namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Logical;

    public class GraphQLQueryContextConfiguration : IGraphQLQueryContextConfiguration
    {
        public ILogicalContext LogicalContext { get; private set; }
        
        public IGraphQLQueryContextExtension[] Extensions { get; private set; }

        public GraphQLQueryContextConfiguration()
        {
            Extensions = new IGraphQLQueryContextExtension[0];
        }

        public IGraphQLQueryContextConfiguration Use(IGraphQLQueryContextExtension[] extensions)
        {
            if (extensions == null)
            {
                throw new ArgumentException(nameof(extensions));
            }

            Extensions = extensions
                .Concat(Extensions)
                .Distinct()
                .ToArray();
            return this;
        }

        public IGraphQLQueryContextConfiguration Use(ILogicalContext logicalContext)
        {
            LogicalContext = logicalContext;
            return this;
        }
    }
}