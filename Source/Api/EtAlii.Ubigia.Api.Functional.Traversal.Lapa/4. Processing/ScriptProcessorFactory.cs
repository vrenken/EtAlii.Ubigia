// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;

    internal class ScriptProcessorFactory : Factory<IScriptProcessor, ScriptProcessorConfiguration, IScriptProcessorExtension>, IScriptProcessorFactory
    {
        protected override IScaffolding[] CreateScaffoldings(ScriptProcessorConfiguration configuration)
        {
            return new IScaffolding[]
            {
                new ScriptProcessingScaffolding(configuration),
                new ScriptExecutionPlanningScaffolding(),
                new SubjectProcessingScaffolding(configuration.FunctionHandlersProvider),
                new RootProcessingScaffolding(configuration.RootHandlerMappersProvider),
                new PathBuildingScaffolding(),
                new ConstantHelpersScaffolding(),
                new OperatorProcessingScaffolding(),
                new ProcessingSelectorsScaffolding(),
                new FunctionSubjectProcessingScaffolding(),

                new LapaPathBuildingScaffolding(),

                // Script Parsing
                new ScriptParserScaffolding(),

                // Additional processing (for path variable parts).
                new PathSubjectParsingScaffolding(),
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
