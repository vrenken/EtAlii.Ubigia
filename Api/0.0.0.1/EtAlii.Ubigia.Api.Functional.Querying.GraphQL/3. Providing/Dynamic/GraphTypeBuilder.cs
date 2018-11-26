namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using global::GraphQL.Resolvers;
    using global::GraphQL.Types;

    internal class GraphTypeBuilder : IGraphTypeBuilder
    {

        public void Build(
            string path, 
            string name,
            PropertyDictionary properties, 
            out DynamicObjectGraphType graphType, 
            out FieldType fieldType)
        {
            graphType = DynamicObjectGraphType.Create(path, name, properties);

            var fieldTypeInstanceReference = graphType;
            fieldType = new FieldType()
            {
                Name = graphType.Name,
                Description = $"Field created for the Ubigia path: {path}",
                Type = graphType.GetType(),
                Arguments = null,
                Resolver = new FuncFieldResolver<object, object>(_ => fieldTypeInstanceReference)
            };
        }
        
        public void Build(
            string path, 
            string name,
            object value,
            out ScalarGraphType graphType,
            out FieldType fieldType)
        {
            graphType = DynamicObjectGraphType.GetScalarGraphType(value);
            
            fieldType = new FieldType()
            {
                Name = name,
                Description = $"Field created for the Ubigia path: {path}",
                Type = DynamicObjectGraphType.GetType(value),
                Arguments = null,
                Resolver = DynamicObjectGraphType.GetResolver(value)
            };
        }
    }
}
