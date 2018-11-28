namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Collections;
    using global::GraphQL.Resolvers;
    using global::GraphQL.Types;

    internal class ListFieldTypeBuilder : IListFieldTypeBuilder
    {
        public FieldType Build(
            string path, 
            string name,
            IInternalNode[] nodes,
            out GraphType graphType)
        {
            var propertiesCollections = nodes
                .Select(node => node.GetProperties())
                .ToArray();

            var listItemProperties = MergeProperties(propertiesCollections);
            graphType = DynamicObjectGraphType.CreateShallow(path, name, listItemProperties);

            var dynamicObjects = propertiesCollections
                .Select(DynamicObject.CreateInstance)
                .ToArray();
                
            var result = new FieldType
            {
                Name = name,
                ResolvedType = new ListGraphType(graphType),
                Resolver = new InstanceFieldResolver(dynamicObjects),
            };

            return result;
        }
        
        private PropertyDictionary MergeProperties(PropertyDictionary[] propertiesCollection)
        {
            return propertiesCollection.First();
//            var result = new PropertyDictionary();
//
//            var uniqueProperties = propertiesCollection
//                .SelectMany(properties => properties.Keys)
//                .Distinct()
//                .Select(key => new KeyValuePair<string, object>(key, null));
//            result.AddRange(uniqueProperties );
//            return result;
        }
    }
}
