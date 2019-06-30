namespace EtAlii.Ubigia.Api.Functional.Diagnostics.Querying
{
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;

    internal interface IProfilingQueryProcessor : IQueryProcessor
    {
        IProfiler Profiler { get; }
    }
}