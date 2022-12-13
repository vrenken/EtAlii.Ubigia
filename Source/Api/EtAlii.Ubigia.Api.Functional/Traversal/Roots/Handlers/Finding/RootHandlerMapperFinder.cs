// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;
    using System.Threading.Tasks;

    internal class RootHandlerMapperFinder : IRootHandlerMapperFinder
    {
        private readonly IRootHandlerMappersProvider _rootHandlerMappersProvider;
        private readonly IScriptProcessingContext _processingContext;

        public RootHandlerMapperFinder(
            IScriptProcessingContext processingContext,
            IRootHandlerMappersProvider rootHandlerMappersProvider)
        {
            _processingContext = processingContext;
            _rootHandlerMappersProvider = rootHandlerMappersProvider;
        }

        public async Task<IRootHandlerMapper> Find(string rootName)
        {
            var root = await _processingContext.Logical.Roots
                .Get(rootName)
                .ConfigureAwait(false);

            var rootHandlerMapper = _rootHandlerMappersProvider.RootHandlerMappers.SingleOrDefault(rhp => rhp.Type == root.Type);
            if (rootHandlerMapper == null)
            {
                throw new ScriptParserException($"No root handler found for root '{rootName}' with type '{root.Type}'");
            }
            return rootHandlerMapper;
        }
    }
}
