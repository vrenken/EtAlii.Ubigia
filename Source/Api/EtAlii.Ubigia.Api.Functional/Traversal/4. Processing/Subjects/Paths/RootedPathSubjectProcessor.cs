// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Threading.Tasks;

    internal class RootedPathSubjectProcessor : IRootedPathSubjectProcessor
    {
        private readonly IRootPathProcessor _rootPathProcessor;
        private readonly IPathVariableExpander _pathVariableExpander;

        public RootedPathSubjectProcessor(
            IRootPathProcessor rootPathProcessor,
            IPathVariableExpander pathVariableExpander)
        {
            _rootPathProcessor = rootPathProcessor;
            _pathVariableExpander = pathVariableExpander;
        }

        public async Task Process(Subject subject, ExecutionScope scope, IObserver<object> output)
        {
            var pathSubject = (RootedPathSubject) subject;

            // Let's expand all possible variables within the path.
            var parts = await _pathVariableExpander
                .Expand(scope, pathSubject.Parts)
                .ConfigureAwait(false);

            // And handover the root and following path for root path processing.
            await _rootPathProcessor
                .Process(pathSubject.Root, parts, scope, output)
                .ConfigureAwait(false);
        }
    }
}
