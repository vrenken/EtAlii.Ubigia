namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using EtAlii.Ubigia.Api.Logical;
    using global::GraphQL.Types;

    internal interface IComplexFieldTypeBuilder
    {
        FieldType Build(
            string path, 
            string name, 
            IInternalNode node,
            out GraphType graphType);
    }
}