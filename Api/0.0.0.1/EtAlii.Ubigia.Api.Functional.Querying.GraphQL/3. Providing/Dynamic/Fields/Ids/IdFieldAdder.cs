namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using global::GraphQL.Types;

    class IdFieldAdder : IIdFieldAdder
    {
        private readonly IScalarFieldTypeBuilder _scalarFieldTypeBuilder;

        public IdFieldAdder(IScalarFieldTypeBuilder scalarFieldTypeBuilder)
        {
            _scalarFieldTypeBuilder = scalarFieldTypeBuilder;
        }

        public void Add(
            string name,
            IdDirectiveResult idDirectiveResult, 
            Context context,
            GraphType parent,
            Dictionary<System.Type, GraphType> graphTypes)
        {
        
            var mappings = idDirectiveResult.Mappings;
            
            if(!parent.Metadata.TryGetValue(ListFieldTypeBuilder.DynamicObjectsMetadataKey, out var value))
            {
                if (mappings.Single() is var mapping)
                {
                    var fieldType = _scalarFieldTypeBuilder.Build(idDirectiveResult.Path, name, mapping.Id, out var graphType);
                    ((ComplexGraphType<object>)parent).AddField(fieldType);
            
                    if (graphType != null)
                    {
                        context.GraphType = graphType;
                        graphTypes[graphType.GetType()] = graphType;
                    }    
                }
            }
            else
            {
                var dynamicObjects = (IDictionary<Identifier, DynamicObjectTuple>) value;
                foreach (var mapping in mappings)
                {
                    if (dynamicObjects.TryGetValue(mapping.Identifier, out var matchingTuple))
                    {
                        var properties = matchingTuple.Properties;
                        var clonedProperties = new PropertyDictionary(properties) { {name, mapping.Id } };
                        matchingTuple.Properties = clonedProperties;
                        matchingTuple.Instance = DynamicObject.CreateInstance(clonedProperties);
                    }
                }
            
                var fieldType = _scalarFieldTypeBuilder.BuildShallow(idDirectiveResult.Path, name, String.Empty);
                
                var listGraphType = (ComplexGraphType<object>)((ListGraphType)parent).ResolvedType;
                listGraphType.AddField(fieldType);
            }
        }
    }
}