namespace EtAlii.Ubigia.Api.Functional.Diagnostics.Scripting
{
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Functional;

    internal interface IProfilingScriptProcessor : IScriptProcessor
    {
        IProfiler Profiler { get; }
    }
}