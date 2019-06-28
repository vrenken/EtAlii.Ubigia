namespace EtAlii.Ubigia.Api.Functional
{
    using System.Threading.Tasks;

    public interface IGraphQLQueryContext
    {
        Task<QueryParseResult> Parse(string text);
        
        Task<GraphQLQueryProcessingResult> Process(Query query);
    }
}