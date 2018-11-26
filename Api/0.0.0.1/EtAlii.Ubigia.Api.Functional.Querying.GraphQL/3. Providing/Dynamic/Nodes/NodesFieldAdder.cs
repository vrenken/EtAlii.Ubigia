namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using global::GraphQL.Types;

    class NodesFieldAdder : INodesFieldAdder
    {
        private readonly IGraphTypeBuilder _graphTypeBuilder;

        public NodesFieldAdder(IGraphTypeBuilder graphTypeBuilder)
        {
            _graphTypeBuilder = graphTypeBuilder;
        }

        public void Add(
            string name,
            NodesDirectiveResult[] nodesDirectiveResults, 
            Registration registration, 
            ComplexGraphType<object> parent, 
            Dictionary<System.Type, GraphType> graphTypes)
        {
            var nodesDirectiveResult = nodesDirectiveResults.FirstOrDefault();
            if (nodesDirectiveResult != null)
            {
                var properties = nodesDirectiveResult.Nodes.First().GetProperties();
                _graphTypeBuilder.Build(nodesDirectiveResult.Path, name, properties, out DynamicObjectGraphType graphType, out FieldType fieldType);

                registration.GraphType = graphType;
                registration.FieldType = fieldType;
                
                graphTypes[graphType.GetType()] = graphType;
                
                parent.AddField(fieldType);
            }
        }
    }
}