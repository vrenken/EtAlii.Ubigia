namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Logical;
    using GraphQL.Resolvers;
    using GraphQL.Types;

    internal class ListFieldTypeBuilder : IListFieldTypeBuilder
    {
        public const string DynamicObjectsMetadataKey = "DynamicObjects";
        
        public FieldType Build(
            string path, 
            string name,
            IInternalNode[] nodes,
            out GraphType graphType)
        {
            var dynamicObjects = nodes
                .Select(node => new DynamicObjectTuple
                {
                    Identifier = node.Id,
                    Properties = node.GetProperties(),
                })
                .Select(m =>
                {
                    m.Instance = DynamicObject.CreateInstance(m.Properties);
                    return m;
                })
                .ToDictionary(m => m.Identifier, m => m);
         
            var propertiesCollections = dynamicObjects.Values
                .Select(m => m.Properties)
                .ToArray();

            var listItemProperties = MergeProperties(propertiesCollections);
            var listGraphType = DynamicObjectGraphType.CreateShallow(path, name, listItemProperties);

            graphType = new ListGraphType(listGraphType) { Metadata = {[DynamicObjectsMetadataKey] = dynamicObjects} };

            var result = new FieldType
            {
                Name = name,
                ResolvedType = graphType,
                Resolver = new FuncFieldResolver<object>(_ => dynamicObjects.Values.Select(v => v.Instance)),
            };

            return result;
        }
        
        private PropertyDictionary MergeProperties(PropertyDictionary[] propertiesCollection)
        {
            return propertiesCollection.First();
//            var result = new PropertyDictionary()
//
//            var uniqueProperties = propertiesCollection
//                .SelectMany(properties => properties.Keys)
//                .Distinct()
//                .Select(key => new KeyValuePair<string, object>(key, null))
//            result.AddRange(uniqueProperties )
//            return result
        }
    }
}
