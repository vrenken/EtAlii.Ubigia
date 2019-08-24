namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System;
    using GraphQL.Types;

    internal class Context
    {
        public Guid Id { get; protected set; }
        public NodesDirectiveResult[] NodesDirectiveResults { get; protected set; }
        public GraphType GraphType { get; set;} 
    }
}