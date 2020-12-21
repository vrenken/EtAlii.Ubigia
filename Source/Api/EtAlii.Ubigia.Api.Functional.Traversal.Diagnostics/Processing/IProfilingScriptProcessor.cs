namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Diagnostics.Profiling;

    internal interface IProfilingScriptProcessor : IScriptProcessor
    {
        IProfiler Profiler { get; }
    }
}
