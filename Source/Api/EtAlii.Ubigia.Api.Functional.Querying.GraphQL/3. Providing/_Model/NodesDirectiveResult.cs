namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using EtAlii.Ubigia.Api.Logical;

    internal class NodesDirectiveResult : DirectiveResult
    {
        public IInternalNode[] Nodes { get; set; }
    }
}