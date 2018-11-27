namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System.Collections.Generic;
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

            var itemGraphTypes = DynamicObjectGraphType.Create(path, name, propertiesCollection);
            
            var listItemProperties = MergeProperties(propertiesCollection);

            var listItemGraphType = DynamicObjectGraphType.CreateShallow(path, name, listItemProperties);

            graphType = listItemGraphType;
            
            var result = new FieldType
            {
                Name = name,
                ResolvedType = new ListGraphType(listItemGraphType),
                Resolver = new FuncFieldResolver<object, object>(context => propertiesCollection),
            };

//            
//            var itemFieldTypes = new List<FieldType>();
//            GraphType firstGraphType = null;
//            
//            foreach (var properties in propertiesCollection)
//            {
//                var itemFieldType = _complexFieldTypeBuilder.Build(path, name, properties, graphTypes, out var graphType);
//                itemFieldTypes.Add(itemFieldType);
//                if (firstGraphType == null)
//                {
//                    firstGraphType = graphType;
//                }
//            }
//            
//            var listGraphType = new ListGraphType(firstGraphType);
//            //graphTypes[listGraphType.GetType()] = listGraphType;
//
//            var listFieldType = new FieldType
//            {
//                Name = name,
//                Description = $"Array field created for the Ubigia path: {path}",
//                Type = listGraphType.GetType(),
//                Arguments = null,
//                Resolver = new FuncFieldResolver<object, object[]>(context => itemFieldTypes.Cast<object>().ToArray())
//            };
//            return listFieldType;
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
