namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using EtAlii.Ubigia.Api.Logical;

    internal class NodesDirectiveResult : DirectiveResult
    {
        public IInternalNode[] Nodes { get; set; }
    }
}