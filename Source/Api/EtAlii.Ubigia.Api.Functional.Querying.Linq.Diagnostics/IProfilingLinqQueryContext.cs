namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using EtAlii.Ubigia.Diagnostics.Profiling;

    public interface IProfilingLinqQueryContext : ILinqQueryContext, IProfilingContext
    {
    }
}