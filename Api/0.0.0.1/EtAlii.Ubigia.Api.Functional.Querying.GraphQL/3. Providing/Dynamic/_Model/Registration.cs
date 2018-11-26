namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using global::GraphQL.Types;

    internal class Registration
    {
        public Guid Id { get; protected set; }
        public NodesDirectiveResult[] NodesDirectiveResults { get; protected set; }
                
        public FieldType FieldType { get; set; }
        public GraphType GraphType { get; set; } 
    }
}