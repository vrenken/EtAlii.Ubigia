namespace EtAlii.Ubigia.Api.Functional
{
    using System.Threading.Tasks;
    using GraphQL;

    public interface IGraphQLQueryContext
    {
        Task<QueryExecutionResult> Execute(string query);//, Inputs inputs);
    }
}