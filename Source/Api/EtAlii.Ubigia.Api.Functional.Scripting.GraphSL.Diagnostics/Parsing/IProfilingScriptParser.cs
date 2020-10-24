namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using EtAlii.Ubigia.Diagnostics.Profiling;

    internal interface IProfilingScriptParser : IScriptParser
    {
        IProfiler Profiler { get; }
    }
}