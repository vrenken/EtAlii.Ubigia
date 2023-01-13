// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;
using System.Linq;
using System.Threading.Tasks;

internal class AbsolutePathSubjectProcessor : IAbsolutePathSubjectProcessor
{
    private readonly IRootPathProcessor _rootPathProcessor;
    private readonly IPathVariableExpander _pathVariableExpander;
    private readonly IPathSubjectForOutputConverter _converter;
    private readonly IPathSubjectPartContentGetter _partContentGetter;

    public AbsolutePathSubjectProcessor(
        IRootPathProcessor rootPathProcessor,
        IPathVariableExpander pathVariableExpander,
        IPathSubjectForOutputConverter converter,
        IPathSubjectPartContentGetter partContentGetter)
    {
        _rootPathProcessor = rootPathProcessor;
        _pathVariableExpander = pathVariableExpander;
        _converter = converter;
        _partContentGetter = partContentGetter;
    }

    public async Task Process(Subject subject, ExecutionScope scope, IObserver<object> output)
    {
        var pathSubject = (AbsolutePathSubject)subject;

        // Let's expand all possible variables within the path.
        var parts = await _pathVariableExpander.Expand(scope, pathSubject.Parts).ConfigureAwait(false);

        if (parts[1] is IdentifierPathSubjectPart)
        {
            // Straight conversion of all paths that start with an identifier.
            _converter.Convert(pathSubject, scope, output);

        }
        else
        {
            // Ok, we can translate the path into a rooted path. let's do so.
            var root = await _partContentGetter
                .GetPartContent(parts.Skip(1).First(), scope)
                .ConfigureAwait(false);
            var path = parts.Length > 3
                ? parts.Skip(3).ToArray()
                : Array.Empty<PathSubjectPart>();

            await _rootPathProcessor
                .Process(root, path, scope, output)
                .ConfigureAwait(false);
        }
    }
}
