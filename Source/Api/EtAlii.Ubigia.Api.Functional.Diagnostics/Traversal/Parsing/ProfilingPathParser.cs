// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using EtAlii.Ubigia.Diagnostics.Profiling;

    internal class ProfilingPathParser : IProfilingPathParser
    {
        public IProfiler Profiler { get; }

        private readonly IPathParser _decoree;

        public ProfilingPathParser(
            IPathParser decoree,
            IProfiler profiler)
        {
            _decoree = decoree;
            Profiler = profiler.Create(ProfilingAspects.Functional.ScriptParser);
        }

        public Subject ParsePath(string text)
        {
            Subject result;
            try
            {
                result = _decoree.ParsePath(text);

            }
            catch (Exception e)
            {
                // Let's show an error message in the profiling view if we encountered an exception.
                dynamic exceptionProfile = Profiler.Begin("Error: " + e.Message);
                exceptionProfile.Error = e.Message;
                Profiler.End(exceptionProfile);

                throw;
            }

            return result;
        }

        public Subject ParseNonRootedPath(string text)
        {
            Subject result;
            try
            {
                result = _decoree.ParseNonRootedPath(text);

            }
            catch (Exception e)
            {
                // Let's show an error message in the profiling view if we encountered an exception.
                dynamic exceptionProfile = Profiler.Begin("Error: " + e.Message);
                exceptionProfile.Error = e.Message;
                Profiler.End(exceptionProfile);

                throw;
            }

            return result;
        }

        public Subject ParseRootedPath(string text)
        {
            Subject result;
            try
            {
                result = _decoree.ParseRootedPath(text);

            }
            catch (Exception e)
            {
                // Let's show an error message in the profiling view if we encountered an exception.
                dynamic exceptionProfile = Profiler.Begin("Error: " + e.Message);
                exceptionProfile.Error = e.Message;
                Profiler.End(exceptionProfile);

                throw;
            }

            return result;
        }
    }
}
