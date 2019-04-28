namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System.Collections.Generic;
    using System.Linq;
    using global::GraphQL.Types;

    class NodesFieldAdder : INodesFieldAdder
    {
        private readonly IComplexFieldTypeBuilder _complexFieldTypeBuilder;
        //private readonly IScalarFieldTypeBuilder _scalarFieldTypeBuilder
        private readonly IListFieldTypeBuilder _listFieldTypeBuilder;

        public NodesFieldAdder(
            IComplexFieldTypeBuilder complexFieldTypeBuilder, 
            //IScalarFieldTypeBuilder scalarFieldTypeBuilder, 
            IListFieldTypeBuilder listFieldTypeBuilder)
        {
            _complexFieldTypeBuilder = complexFieldTypeBuilder;
            //_scalarFieldTypeBuilder = scalarFieldTypeBuilder
            _listFieldTypeBuilder = listFieldTypeBuilder;
        }

        public void Add(
            string name,
            NodesDirectiveResult[] nodesDirectiveResults, 
            FieldContext context, 
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
                        fieldType = _complexFieldTypeBuilder.Build(path, name, null, out graphType);
                        break;
                    case 1: 
                        var node = nodesDirectiveResult.Nodes.Single();
                        fieldType = _complexFieldTypeBuilder.Build(path, name, node, out graphType);
                        break;
                    default:
                        var nodes = nodesDirectiveResult.Nodes;
                        fieldType = _listFieldTypeBuilder.Build(path, name, nodes, out graphType);
                        break;
                }

                if (parent is ListGraphType listGraphType)
                {
                    ((ComplexGraphType<object>)listGraphType.ResolvedType).AddField(fieldType);        
                }
                else
                {
                    ((ComplexGraphType<object>)parent).AddField(fieldType);        
                }
                
                if (graphType != null)
                {
                    context.GraphType = graphType;
                    graphTypes[graphType.GetType()] = graphType;
                }
            }
        }
    }
}