namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using EtAlii.Ubigia.Api.Logical;
    using GraphQL.Types;

    internal interface IComplexFieldTypeBuilder
    {
        FieldType Build(
            string path, 
            string name, 
            IInternalNode node,
            out GraphType graphType);
    }
}