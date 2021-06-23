// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Diagnostics.Profiling
{
    using System.Collections.Generic;

    public interface IProfilingResultStack : IEnumerable<ProfilingResult>
    {
        ProfilingResult Peek();

        void Push(ProfilingResult profilingResult);
        ProfilingResult Pop();
    }
}