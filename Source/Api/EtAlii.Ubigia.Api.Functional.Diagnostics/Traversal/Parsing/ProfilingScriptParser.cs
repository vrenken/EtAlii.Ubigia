// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System.Linq;
using EtAlii.Ubigia.Diagnostics.Profiling;

internal class ProfilingScriptParser : IProfilingScriptParser
{
    public IProfiler Profiler { get; }

    private readonly IScriptParser _decoree;

    public ProfilingScriptParser(
        IScriptParser decoree,
        IProfiler profiler)
    {
        _decoree = decoree;
        Profiler = profiler.Create(ProfilingAspects.Functional.ScriptParser);
    }

    public ScriptParseResult Parse(string text, ExecutionScope scope)
    {
        var result = _decoree.Parse(text, scope);

        var errorMessage = result.Errors
            .Select(e => e.Message)
            .FirstOrDefault();
        if (errorMessage != null)
        {
            // Let's show an error message in the profiling view if we encountered an exception.
            dynamic exceptionProfile = Profiler.Begin("Error: " + errorMessage);
            exceptionProfile.Error = errorMessage;
            Profiler.End(exceptionProfile);
        }

        return result;
    }

    public ScriptParseResult Parse(string[] text, ExecutionScope scope)
    {
        var result = _decoree.Parse(text, scope);

        var errorMessage = result.Errors
            .Select(e => e.Message)
            .FirstOrDefault();
        if (errorMessage != null)
        {
            // Let's show an error message in the profiling view if we encountered an exception.
            dynamic exceptionProfile = Profiler.Begin("Error: " + errorMessage);
            exceptionProfile.Error = errorMessage;
            Profiler.End(exceptionProfile);
        }

        return result;
    }
}
