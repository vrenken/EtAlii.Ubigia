namespace EtAlii.Ubigia.Api.Functional
{
    using System.Threading.Tasks;
    using GraphQL;

    public interface IGraphQLQueryContext
    {
        Task<ExecutionResult> Execute(string operationName, string query, Inputs inputs);
    }
}