namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using global::GraphQL.Types;

    internal class Registration
    {
        public Guid Id { get; protected set; }
        public NodesDirective[] Directives { get; protected set; }
                
        public FieldType FieldType { get; set; }
        public DynamicObjectGraphType FieldTypeInstance { get; set; } 
    }
}