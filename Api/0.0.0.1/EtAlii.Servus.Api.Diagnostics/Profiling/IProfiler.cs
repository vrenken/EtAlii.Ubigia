namespace EtAlii.Servus.Api.Diagnostics.Profiling
{
    using System;

    public interface IProfiler
    {
        IProfiler Parent { get; }
        IProfiler Previous { get; }
        IProfilingResultStack ResultStack { get; }

        ProfilingAspect Aspect { get; }

        ProfilingAspect[] Aspects { get; set; }

        ProfilingResult Begin(string action);
        void End(ProfilingResult profile);

        event Action<ProfilingResult> ProfilingStarted;
        event Action<ProfilingResult> ProfilingEnded;

        void SetPrevious(IProfiler previous);
        IProfiler Create(ProfilingAspect aspect);
    }
}