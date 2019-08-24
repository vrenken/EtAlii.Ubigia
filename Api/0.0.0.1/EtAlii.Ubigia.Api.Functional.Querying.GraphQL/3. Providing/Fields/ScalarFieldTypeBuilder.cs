namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using GraphQL.Types;

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

        public FieldType BuildShallow(
            string path, 
            string name,
            object value)
        {
            var field = new FieldType
            {
                Name = name,
                Description = $"Shallow field created for the Ubigia path: {path}",
                ResolvedType = GraphTypeConverter.ToScalarGraphType(value),
                Resolver = null,
            };
            return field;
        }
    }
}
