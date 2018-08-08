namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using global::GraphQL.Language.AST;
    using global::GraphQL.Types;

    public interface IFieldTypeBuilder
    {
        FieldType Build(PropertyDictionary properties, string path, Directive directive);
    }
}