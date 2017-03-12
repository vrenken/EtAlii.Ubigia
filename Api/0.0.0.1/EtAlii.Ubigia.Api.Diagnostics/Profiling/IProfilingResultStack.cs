namespace EtAlii.Ubigia.Api.Diagnostics.Profiling
{
    using System.Collections.Generic;

    public interface IProfilingResultStack : IEnumerable<ProfilingResult>
    {
        ProfilingResult Peek();

        void Push(ProfilingResult profilingResult);
        ProfilingResult Pop();
    }
}