// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using EtAlii.xTechnology.MicroContainer;

    internal class ScriptProcessorFactory : Factory<IScriptProcessor, TraversalProcessorConfiguration, IScriptProcessorExtension>, IScriptProcessorFactory
    {
        protected override IScaffolding[] CreateScaffoldings(TraversalProcessorConfiguration configuration)
        {
            return Array.Empty<IScaffolding>();
        }

        protected override void InitializeInstance(IScriptProcessor instance, Container container)
        {
            var pathProcessor = container.GetInstance<IPathProcessor>();
            var pathSubjectToGraphPathConverter = container.GetInstance<IPathSubjectToGraphPathConverter>();

            var absolutePathSubjectProcessor = container.GetInstance<IAbsolutePathSubjectProcessor>();
            var relativePathSubjectProcessor = container.GetInstance<IRelativePathSubjectProcessor>();
            var rootedPathSubjectProcessor = container.GetInstance<IRootedPathSubjectProcessor>();

            var pathSubjectForOutputConverter = container.GetInstance<IPathSubjectForOutputConverter>();
            var addRelativePathToExistingPathProcessor = container.GetInstance<IAddRelativePathToExistingPathProcessor>();

            container.GetInstance<IScriptProcessingContext>().Initialize(pathSubjectToGraphPathConverter, absolutePathSubjectProcessor, relativePathSubjectProcessor, rootedPathSubjectProcessor, pathProcessor, pathSubjectForOutputConverter, addRelativePathToExistingPathProcessor);
        }
    }
}
