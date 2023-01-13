// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;
using System.Threading.Tasks;

internal class RootPathProcessor : IRootPathProcessor
{
    private readonly IScriptProcessingContext _processingContext;
    private readonly IRootHandlerMapperFinder _rootHandlerMapperFinder;
    private readonly IRootHandlerPathMatcher _rootHandlerPathMatcher;

    public RootPathProcessor(
        IScriptProcessingContext processingContext,
        IRootHandlerMapperFinder rootHandlerMapperFinder,
        IRootHandlerPathMatcher rootHandlerPathMatcher)
    {
        _processingContext = processingContext;
        _rootHandlerMapperFinder = rootHandlerMapperFinder;
        _rootHandlerPathMatcher = rootHandlerPathMatcher;
    }

    public async Task Process(string root, PathSubjectPart[] path, ExecutionScope scope, IObserver<object> output)
    {
        // Find root handler mapper.
        var rootHandlerMapper = await _rootHandlerMapperFinder
            .Find(root)
            .ConfigureAwait(false);
        if (rootHandlerMapper == null)
        {
            throw new InvalidOperationException("No matching root handler mapper found.");
        }
        // Find the matching root handler.
        MatchResult match = null;
        foreach (var rh in rootHandlerMapper.AllowedRootHandlers)
        {
            var m = await _rootHandlerPathMatcher
                .Match(scope, rh, path)
                .ConfigureAwait(false);
            if (m != MatchResult.NoMatch)
            {
                match = m;
                break;
            }
        }
        if (match == null)
        {
            throw new InvalidOperationException("No matching root handler found.");
        }

        // And process...
        var rootHandler = match.RootHandler;
        rootHandler.Process(_processingContext, root, match.Match, match.Rest, scope, output);
    }
}
