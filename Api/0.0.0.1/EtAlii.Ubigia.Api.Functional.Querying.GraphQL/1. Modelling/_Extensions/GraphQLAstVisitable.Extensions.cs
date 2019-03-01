namespace EtAlii.Ubigia.Api.Functional
{
    using GraphQL.Language.AST;

    internal static class GraphQLAstVisitableExtensions
    {
        public static object Accept(this GraphQL.Language.AST.Document document, IGraphQLAstVisitor visitor)
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