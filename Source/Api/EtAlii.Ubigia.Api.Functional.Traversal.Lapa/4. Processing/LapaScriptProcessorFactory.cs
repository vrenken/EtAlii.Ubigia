// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;

    internal class LapaScriptProcessorFactory : Factory<IScriptProcessor, ScriptProcessorConfiguration, IScriptProcessorExtension>, IScriptProcessorFactory
    {
        protected override IScaffolding[] CreateScaffoldings(ScriptProcessorConfiguration configuration)
        {
            return new IScaffolding[]
            {
                // Processing.
                new ScriptProcessingScaffolding(configuration),
                new ScriptExecutionPlanningScaffolding(),
                new SubjectProcessingScaffolding(configuration.FunctionHandlersProvider),
                new RootProcessingScaffolding(configuration.RootHandlerMappersProvider),
                new PathBuildingScaffolding(),
                new OperatorProcessingScaffolding(),
                new ProcessingSelectorsScaffolding(),
                new FunctionSubjectProcessingScaffolding(),

                // Script Parsing
                // TODO: These should actually be converted into a single+dedicated LapaScriptParser instance registration.
                // However, for now this move is too big.
                new LapaScriptParserScaffolding(),
                new LapaPathSubjectParsingScaffolding(),
                new LapaConstantParsingScaffolding(),
                new LapaSequenceParsingScaffolding(),
                new LapaSubjectParsingScaffolding(),
                new LapaOperatorParsingScaffolding(),
            };
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
