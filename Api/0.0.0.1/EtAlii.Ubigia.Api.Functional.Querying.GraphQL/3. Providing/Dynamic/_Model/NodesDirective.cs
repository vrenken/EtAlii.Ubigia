namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api.Logical;
    using global::GraphQL.Types;

    internal class NodesDirective
    {
        public string Path { get; set; }
        public IEnumerable<IInternalNode> Nodes { get; set; }
    }
}