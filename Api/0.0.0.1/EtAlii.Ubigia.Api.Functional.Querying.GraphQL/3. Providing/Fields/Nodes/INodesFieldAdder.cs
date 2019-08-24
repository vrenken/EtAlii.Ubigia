namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System.Collections.Generic;
    using GraphQL.Types;

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