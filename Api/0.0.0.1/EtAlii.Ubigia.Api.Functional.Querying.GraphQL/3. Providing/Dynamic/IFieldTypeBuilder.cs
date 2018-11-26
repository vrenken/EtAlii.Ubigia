namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using global::GraphQL.Language.AST;
    using global::GraphQL.Types;

    internal interface IFieldTypeBuilder
    {
        void Build(string path, string name, PropertyDictionary properties, out DynamicObjectGraphType fieldTypeInstance, out FieldType fieldType);

        void Build(string path, string name, object value, out ScalarGraphType fieldTypeInstance, out FieldType fieldType);
    }
}