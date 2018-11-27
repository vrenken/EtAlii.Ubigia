namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System.Collections.Generic;
    using global::GraphQL.Types;

    internal interface IComplexFieldTypeBuilder
    {
        FieldType Build(
            string path, 
            string name, 
            PropertyDictionary properties, 
            Dictionary<System.Type, GraphType> graphTypes, out GraphType graphType);
    }
}