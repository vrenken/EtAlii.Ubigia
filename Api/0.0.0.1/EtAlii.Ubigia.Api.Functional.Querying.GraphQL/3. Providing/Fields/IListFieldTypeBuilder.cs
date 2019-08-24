namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using EtAlii.Ubigia.Api.Logical;
    using GraphQL.Types;

    internal interface IListFieldTypeBuilder
    {
        FieldType Build(
            string path,
            string name,
            IInternalNode[] nodes,
            out GraphType graphType);
    }
}