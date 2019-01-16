namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using global::GraphQL.Types;

    internal class DynamicObjectTuple
    {
        public Identifier Identifier { get; set; }
        public PropertyDictionary Properties { get; set; }
        public object Instance { get; set;} 
    }
}