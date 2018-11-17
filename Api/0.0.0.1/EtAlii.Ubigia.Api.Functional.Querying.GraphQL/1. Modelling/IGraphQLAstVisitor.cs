namespace EtAlii.Ubigia.Api.Functional
{
    internal interface IGraphQLAstVisitor
    {
        void Visit(GraphQL.Language.AST.Operation operation);
        void Visit(GraphQL.Language.AST.Document document);
    }
}