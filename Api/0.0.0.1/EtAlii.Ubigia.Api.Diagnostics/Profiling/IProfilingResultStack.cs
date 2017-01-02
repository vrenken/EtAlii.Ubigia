namespace EtAlii.Ubigia.Api.Diagnostics.Profiling
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface IProfilingResultStack : IEnumerable<ProfilingResult>
    {
        ProfilingResult Peek();

        void Push(ProfilingResult profilingResult);
        ProfilingResult Pop();
    }
}