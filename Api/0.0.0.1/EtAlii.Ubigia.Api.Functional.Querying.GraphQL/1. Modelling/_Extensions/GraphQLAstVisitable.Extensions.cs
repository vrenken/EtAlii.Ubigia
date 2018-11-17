namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Querying.GraphQL;
    using System.Threading.Tasks;
    using GraphQL;
    using GraphQL.Execution;
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