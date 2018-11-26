namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using global::GraphQL.Types;

    class NodesFieldAdder : INodesFieldAdder
    {
        private readonly IFieldTypeBuilder _fieldTypeBuilder;

        public NodesFieldAdder(IFieldTypeBuilder fieldTypeBuilder)
        {
            _fieldTypeBuilder = fieldTypeBuilder;
        }

        public void Add(
            string name,
            NodesDirectiveResult[] nodesDirectiveResults, 
            Registration registration, 
            ComplexGraphType<object> parent, 
            Dictionary<System.Type, GraphType> graphObjectInstances)
        {
            var nodesDirectiveResult = nodesDirectiveResults.FirstOrDefault();
            if (nodesDirectiveResult != null)
            {
                var properties = nodesDirectiveResult.Nodes.First().GetProperties();
                _fieldTypeBuilder.Build(nodesDirectiveResult.Path, name, properties, out DynamicObjectGraphType fieldTypeInstance, out FieldType fieldType);

                registration.FieldTypeInstance = fieldTypeInstance;
                registration.FieldType = fieldType;
                
                graphObjectInstances[fieldTypeInstance.GetType()] = fieldTypeInstance;
                
                parent.AddField(fieldType);
            }
        }
    }
}