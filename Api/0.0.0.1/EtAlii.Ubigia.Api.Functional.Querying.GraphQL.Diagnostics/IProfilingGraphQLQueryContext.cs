namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Functional.Querying.GraphQL;

    public interface IProfilingGraphQLQueryContext : IGraphQLQueryContext, IProfilingContext
    {
    }
}