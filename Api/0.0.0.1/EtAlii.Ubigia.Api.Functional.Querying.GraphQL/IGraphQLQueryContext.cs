namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Threading.Tasks;
    using GraphQL;
    using GraphQL.Types;

    public interface IGraphQLQueryContext
    {
        Task<ExecutionResult> Execute(string operationName, string query, Inputs inputs);
    }
}