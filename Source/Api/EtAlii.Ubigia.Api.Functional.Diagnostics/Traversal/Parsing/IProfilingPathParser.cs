namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Diagnostics.Profiling;

    internal interface IProfilingPathParser : IPathParser
    {
        IProfiler Profiler { get; }
    }
}
