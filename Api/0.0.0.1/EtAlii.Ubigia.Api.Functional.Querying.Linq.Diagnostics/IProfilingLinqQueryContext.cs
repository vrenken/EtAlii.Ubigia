namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;

    public interface IProfilingLinqQueryContext : ILinqQueryContext, IProfilingContext
    {
    }
}