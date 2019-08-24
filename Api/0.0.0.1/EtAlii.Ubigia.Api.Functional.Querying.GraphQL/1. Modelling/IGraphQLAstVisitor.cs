namespace EtAlii.Ubigia.Api.Functional.Querying
{
    internal interface IGraphQLAstVisitor
    {
        void Visit(GraphQL.Language.AST.Operation operation);
        void Visit(global::GraphQL.Language.AST.Document document);
    }
}