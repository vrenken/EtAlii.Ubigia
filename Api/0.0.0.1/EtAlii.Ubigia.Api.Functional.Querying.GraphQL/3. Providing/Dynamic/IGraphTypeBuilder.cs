namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using global::GraphQL.Language.AST;
    using global::GraphQL.Types;

    internal interface IGraphTypeBuilder
    {
        void Build(
            string path, 
            string name, 
            PropertyDictionary properties, 
            out DynamicObjectGraphType graphType, 
            out FieldType fieldType);

        void Build(
            string path, 
            string name, 
            object value, 
            out ScalarGraphType graphType, 
            out FieldType fieldType);
    }
}