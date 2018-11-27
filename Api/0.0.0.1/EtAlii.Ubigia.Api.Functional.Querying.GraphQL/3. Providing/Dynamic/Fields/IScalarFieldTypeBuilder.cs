namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System.Collections.Generic;
    using global::GraphQL.Types;

    internal interface IScalarFieldTypeBuilder
    {
        FieldType Build(
            string path, 
            string name, 
            object value, 
            Dictionary<System.Type, GraphType> graphTypes, out GraphType graphType);
    }
}