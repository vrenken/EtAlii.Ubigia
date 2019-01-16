namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api.Logical;
    using global::GraphQL.Types;

    internal class NodesDirectiveResult : DirectiveResult
    {
        public IInternalNode[] Nodes { get; set; }
    }
}