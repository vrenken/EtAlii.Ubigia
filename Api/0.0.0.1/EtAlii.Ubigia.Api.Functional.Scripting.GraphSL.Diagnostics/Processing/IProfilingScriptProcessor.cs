namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using EtAlii.Ubigia.Diagnostics.Profiling;

    internal interface IProfilingScriptProcessor : IScriptProcessor
    {
        IProfiler Profiler { get; }
    }
}