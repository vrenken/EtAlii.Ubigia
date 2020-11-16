namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using GraphQL.Types;

    internal interface INodesFieldAdder
    {
        void Add(
            string name,
            NodesDirectiveResult[] nodesDirectiveResults, 
            FieldContext context, 
            GraphType parent, 
            IGraphTypeServiceProvider graphTypes);
    }
}