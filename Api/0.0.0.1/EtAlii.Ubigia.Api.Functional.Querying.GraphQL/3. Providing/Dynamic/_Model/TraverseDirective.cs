namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api.Logical;
    using global::GraphQL.Types;

    internal class TraverseDirective
    {
        public string Path { get; set; }
        public IEnumerable<IInternalNode> Nodes { get; set; }
        
        public FieldType FieldType { get; set; }
        public DynamicObjectGraphType FieldTypeInstance { get; set; } 
    }
}