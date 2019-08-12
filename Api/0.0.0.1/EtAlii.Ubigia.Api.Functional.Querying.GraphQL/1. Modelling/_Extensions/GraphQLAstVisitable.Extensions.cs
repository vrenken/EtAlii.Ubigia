namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using global::GraphQL.Language.AST;

    internal static class GraphQLAstVisitableExtensions
    {
        public static object Accept(this global::GraphQL.Language.AST.Document document, IGraphQLAstVisitor visitor)
        {
            visitor.Visit(document);
            return null;
        }
        
        public static object Accept(this Operation operation, IGraphQLAstVisitor visitor)
        {
            visitor.Visit(operation);
            return null;
        }
    }
}