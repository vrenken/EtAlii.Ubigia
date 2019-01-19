namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;

    public class FieldDefinition
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public string Path { get; set; }
    }
}