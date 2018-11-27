namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using global::GraphQL.Resolvers;
    using global::GraphQL.Types;

    class NodesFieldAdder : INodesFieldAdder
    {
        private readonly IComplexFieldTypeBuilder _complexFieldTypeBuilder;
        private readonly IScalarFieldTypeBuilder _scalarFieldTypeBuilder;
        private readonly IListFieldTypeBuilder _listFieldTypeBuilder;

        public NodesFieldAdder(
            IComplexFieldTypeBuilder complexFieldTypeBuilder, 
            IScalarFieldTypeBuilder scalarFieldTypeBuilder, 
            IListFieldTypeBuilder listFieldTypeBuilder)
        {
            _complexFieldTypeBuilder = complexFieldTypeBuilder;
            _scalarFieldTypeBuilder = scalarFieldTypeBuilder;
            _listFieldTypeBuilder = listFieldTypeBuilder;
        }

        public void Add(
            string name,
            NodesDirectiveResult[] nodesDirectiveResults, 
            Registration registration, 
            GraphType parent, 
            Dictionary<System.Type, GraphType> graphTypes)
        {
            var nodesDirectiveResult = nodesDirectiveResults.FirstOrDefault();
            if (nodesDirectiveResult != null)
            {
                var path = nodesDirectiveResult.Path; 
                FieldType fieldType;
                GraphType graphType;
                
                var nodeCount = nodesDirectiveResult.Nodes.Length;
                switch (nodeCount)
                {
                    case 0: 
                        fieldType = _complexFieldTypeBuilder.Build(path, name, new PropertyDictionary(), graphTypes, out graphType);
                        break;
                    case 1: 
                        var singleItemProperties = nodesDirectiveResult.Nodes.Single().GetProperties();
                        fieldType = _complexFieldTypeBuilder.Build(path, name, singleItemProperties, graphTypes, out graphType);
                        break;
                    default:
                        fieldType = _listFieldTypeBuilder.Build(path, name, nodesDirectiveResult.Nodes, graphTypes, out graphType);
                        break;
                }
                registration.GraphType = graphType;
                ((ComplexGraphType<object>)parent).AddField(fieldType);        
            }
        }
    }
}