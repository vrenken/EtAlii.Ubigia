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
            Dictionary<System.Type, GraphType> graphTypes, out GraphType graphType)
        {
            graphType = DynamicObjectGraphType.GetScalarGraphType(value);
            graphTypes[graphType.GetType()] = graphType;
            
            var result = new FieldType
            {
                Name = name,
                Description = $"Field created for the Ubigia path: {path}",
                Type = DynamicObjectGraphType.GetType(value),
                Arguments = null,
                Resolver = DynamicObjectGraphType.GetResolver(value)
            };
            return result;
        }
    }
}
