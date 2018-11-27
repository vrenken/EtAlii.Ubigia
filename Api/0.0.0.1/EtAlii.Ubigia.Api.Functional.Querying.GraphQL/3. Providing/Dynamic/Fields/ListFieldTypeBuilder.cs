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
            Dictionary<System.Type, GraphType> graphTypes, out GraphType graphType)
        {
            var propertiesCollection = nodes
                .Select(node => node.GetProperties())
                .ToArray();

            var listItemProperties = MergeProperties(propertiesCollection);
            var listItemGraphType = DynamicObjectGraphType.CreateShallow(path, name, listItemProperties);
            graphType = listItemGraphType;

            var dynamicObjects = propertiesCollection
                .Select(DynamicObject.CreateInstance)
                .ToArray();
                
            var result = new FieldType
            {
                Name = name,
                ResolvedType = new ListGraphType(listItemGraphType),
                Resolver = new FuncFieldResolver<object, object>(context => dynamicObjects),
            };

            return result;
        }

        private ExpandoObject ToDynamicObject(PropertyDictionary properties)
        {           
            var result = new ExpandoObject();
            var dictionary = result as IDictionary<string, object>;
            foreach (var kvp in properties)
            {
                dictionary.Add(kvp.Key, kvp.Value);
            }
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
