namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using global::GraphQL.Resolvers;
    using global::GraphQL.Types;

    internal class FieldTypeBuilder : IFieldTypeBuilder
    {

        public void Build(
            string path, 
            string name,
            PropertyDictionary properties, 
            out DynamicObjectGraphType fieldTypeInstance, 
            out FieldType fieldType)
        {
            fieldTypeInstance = DynamicObjectGraphType.Create(path, name, properties);

            var fieldTypeInstanceReference = fieldTypeInstance;
            fieldType = new FieldType()
            {
                Name = fieldTypeInstance.Name,
                Description = $"Field created for the Ubigia path: {path}",
                Type = fieldTypeInstance.GetType(),
                Arguments = null,
                Resolver = new FuncFieldResolver<object, object>(_ => fieldTypeInstanceReference)
            };
        }
    }
}
