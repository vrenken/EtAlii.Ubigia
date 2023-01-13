// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Diagnostics;

using System;
using System.Threading.Tasks;
using EtAlii.Ubigia.Diagnostics.Profiling;

public sealed class ProfilingGraphPathTraverser : IProfilingGraphPathTraverser
{
    private readonly IGraphPathTraverser _decoree;

    public IProfiler Profiler { get; }

    public ProfilingGraphPathTraverser(IGraphPathTraverser decoree, IProfiler profiler)
    {
        _decoree = decoree;
        Profiler = profiler.Create(ProfilingAspects.Logical.Traversal);
    }

    public async Task<IReadOnlyEntry> TraverseToSingle(Identifier identifier, ExecutionScope scope, bool traverseToFinal = true)
    {
        dynamic profile = Profiler.Begin("TraverseToSingle: " + identifier + " (to final: " + traverseToFinal + ")");
        profile.Identifier = identifier.ToString();
        profile.TraverseToFinal = traverseToFinal;

        var result = await _decoree.TraverseToSingle(identifier, scope, traverseToFinal).ConfigureAwait(false);

        Profiler.End(profile);

        return result;
    }

    public void Traverse(GraphPath path, Traversal traversal, ExecutionScope scope, IObserver<IReadOnlyEntry> output, bool traverseToFinal = true)
    {
        dynamic profile = Profiler.Begin("Traverse: " + traversal + " (to final: " + traverseToFinal + ")");
        profile.Path = path.ToString();
        profile.Traversal = traversal.ToString();
        profile.TraverseToFinal = traverseToFinal;

        _decoree.Traverse(path, traversal, scope, output, traverseToFinal);

        Profiler.End(profile);
    }
}
