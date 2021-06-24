// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class PathProcessor : IPathProcessor
    {
        public IScriptProcessingContext Context { get; }

        private readonly IPathSubjectToGraphPathConverter _pathSubjectToGraphPathConverter;

        public PathProcessor(
            IScriptProcessingContext context,
            IPathSubjectToGraphPathConverter pathSubjectToGraphPathConverter)
        {
            Context = context;
            _pathSubjectToGraphPathConverter = pathSubjectToGraphPathConverter;
        }

        public async Task Process(PathSubject pathSubject, ExecutionScope scope, IObserver<object> output)
        {
            // TODO: Continue here to root processing logical implementation.
            if (pathSubject is RootedPathSubject rootedPathSubject)
            {
                var parts = new List<PathSubjectPart>(rootedPathSubject.Parts);
                var hasParts = parts.Count > 0;
                if (hasParts)
                {
                    parts.Insert(0, new ParentPathSubjectPart());
                }
                parts.Insert(0, new ConstantPathSubjectPart(rootedPathSubject.Root));
                parts.Insert(0, new ParentPathSubjectPart());
                pathSubject = new AbsolutePathSubject(parts.ToArray());
            }

            var graphPath = await _pathSubjectToGraphPathConverter.Convert(pathSubject, scope).ConfigureAwait(false);

            try
            {
                // Path processing should always expect multiple results. So we should always use the Nodes.SelectMany().
                Context.Logical.Nodes.SelectMany(graphPath, scope, output);
            }
            catch (Exception e)
            {
                var message = $"Unable to process query path '{pathSubject}'";
                throw new ScriptProcessingException(message, e);
            }
        }
    }
}
