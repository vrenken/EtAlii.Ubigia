namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using global::GraphQL.Types;

    internal interface IScalarFieldTypeBuilder
    {
        FieldType Build(
            string path, 
            string name, 
            object value, 
            out GraphType graphType);

        FieldType BuildShallow(
            string path,
            string name,
            object value);
    }
}