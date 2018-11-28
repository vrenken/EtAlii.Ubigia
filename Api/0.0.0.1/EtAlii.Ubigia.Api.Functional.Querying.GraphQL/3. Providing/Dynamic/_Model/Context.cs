namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using global::GraphQL.Types;

    internal class Context
    {
        public Guid Id { get; protected set; }
        public NodesDirectiveResult[] NodesDirectiveResults { get; protected set; }
        public GraphType GraphType { get; set;} 
    }
}