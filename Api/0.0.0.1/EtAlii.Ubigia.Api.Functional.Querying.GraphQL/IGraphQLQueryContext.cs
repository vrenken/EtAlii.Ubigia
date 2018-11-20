namespace EtAlii.Ubigia.Api.Functional
{
    using System.Threading.Tasks;
    using GraphQL;

    public interface IGraphQLQueryContext
    {
        Task<ExecutionResult> Execute(string query);//, Inputs inputs);
    }
}