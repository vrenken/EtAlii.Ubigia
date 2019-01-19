namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using EtAlii.Ubigia.Api.Logical;
    using global::GraphQL.Types;

    internal interface IListFieldTypeBuilder
    {
        FieldType Build(
            string path,
            string name,
            IInternalNode[] nodes,
            out GraphType graphType);
    }
}