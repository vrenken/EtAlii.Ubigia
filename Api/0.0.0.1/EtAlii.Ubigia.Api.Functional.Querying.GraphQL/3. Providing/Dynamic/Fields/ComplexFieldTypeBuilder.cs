namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.Ubigia.Api.Logical;
    using global::GraphQL.Resolvers;
    using global::GraphQL.Types;

    internal class ComplexFieldTypeBuilder : IComplexFieldTypeBuilder
    {
        public FieldType Build(
            string path,
            string name,
            IInternalNode node,
            Dictionary<System.Type, GraphType> graphTypes, 
            out GraphType graphType)
        {
            var properties = node?.GetProperties() ?? new PropertyDictionary();
            
            graphType = DynamicObjectGraphType.Create(path, name, properties);
            //graphType.Metadata["EtAlii_Identifier"] = node?.Id ?? Identifier.Empty;
            
            graphTypes[graphType.GetType()] = graphType;

            var scopedgraphType = graphType;
            var result = new FieldType
            {
                Name = graphType.Name,
                Description = $"Field created for the Ubigia path: {path}",
                Type = graphType.GetType(),
                Arguments = null,
                Resolver = new FuncFieldResolver<object, object>(_ => scopedgraphType)
            };
            return result;
        }        
    }
}
