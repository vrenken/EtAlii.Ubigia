// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using EtAlii.xTechnology.MicroContainer;

    internal class ScriptProcessorFactory : Factory<IScriptProcessor, FunctionalOptions, IFunctionalExtension>, IScriptProcessorFactory
    {
        // TODO: Should we remove the ScriptParserFactory and use an injected IScriptParser singleton instance instead?

        protected override IScaffolding[] CreateScaffoldings(FunctionalOptions options)
        {
            return Array.Empty<IScaffolding>();
        }

        protected override void InitializeInstance(IScriptProcessor instance, IServiceCollection services)
        {
            var pathProcessor = services.GetInstance<IPathProcessor>();
            var pathSubjectToGraphPathConverter = services.GetInstance<IPathSubjectToGraphPathConverter>();

            var absolutePathSubjectProcessor = services.GetInstance<IAbsolutePathSubjectProcessor>();
            var relativePathSubjectProcessor = services.GetInstance<IRelativePathSubjectProcessor>();
            var rootedPathSubjectProcessor = services.GetInstance<IRootedPathSubjectProcessor>();

            var pathSubjectForOutputConverter = services.GetInstance<IPathSubjectForOutputConverter>();
            var addRelativePathToExistingPathProcessor = services.GetInstance<IAddRelativePathToExistingPathProcessor>();

            services.GetInstance<IScriptProcessingContext>().Initialize(
                pathSubjectToGraphPathConverter,
                absolutePathSubjectProcessor,
                relativePathSubjectProcessor,
                rootedPathSubjectProcessor,
                pathProcessor,
                pathSubjectForOutputConverter,
                addRelativePathToExistingPathProcessor);
        }
    }
}
