namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System.Collections.Generic;
    using global::GraphQL.Types;

    internal interface INodesFieldAdder
    {
        void Add(
            string name,
            NodesDirectiveResult[] nodesDirectiveResults, 
            FieldContext context, 
            GraphType parent, 
            Dictionary<System.Type, GraphType> graphTypes);
    }
}