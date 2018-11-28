namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System.Collections.Generic;
    using System.Linq;
    using global::GraphQL.Resolvers;
    using global::GraphQL.Types;

    internal class ScalarFieldTypeBuilder : IScalarFieldTypeBuilder
    {    
        public FieldType Build(
            string path, 
            string name,
            object value, 
            out GraphType graphType)
        {
            graphType = null;// We cannot continue traversal beyond this level.
            
            var result = new FieldType
            {
                Name = name,
                Description = $"Field created for the Ubigia path: {path}",
                Type = GraphTypeConverter.ToGraphType(value),
                Arguments = null,
                Resolver = new InstanceFieldResolver(value)
            };
            return result;
        }
    }
}
