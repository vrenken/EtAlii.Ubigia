namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;

    internal interface IProfilingScriptParser : IScriptParser
    {
        IProfiler Profiler { get; }
    }
}