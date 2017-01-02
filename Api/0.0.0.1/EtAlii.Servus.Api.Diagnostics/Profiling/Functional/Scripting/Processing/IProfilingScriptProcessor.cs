namespace EtAlii.Servus.Api.Diagnostics.Profiling
{
    using EtAlii.Servus.Api.Functional;

    internal interface IProfilingScriptProcessor : IScriptProcessor
    {
        IProfiler Profiler { get; }
    }
}