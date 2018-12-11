namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Functional;

    public interface IProfilingGraphQLQueryContext : IGraphQLQueryContext, IProfilingContext
    {
    }
}