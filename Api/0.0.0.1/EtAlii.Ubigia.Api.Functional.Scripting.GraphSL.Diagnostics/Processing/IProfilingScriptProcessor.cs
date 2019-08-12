namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;

    internal interface IProfilingScriptProcessor : IScriptProcessor
    {
        IProfiler Profiler { get; }
    }
}