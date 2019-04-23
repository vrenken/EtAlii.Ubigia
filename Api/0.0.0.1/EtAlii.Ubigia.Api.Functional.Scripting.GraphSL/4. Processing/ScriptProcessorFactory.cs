// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    internal class ScriptProcessorFactory : IScriptProcessorFactory
    {
        public IScriptProcessor Create(IScriptProcessorConfiguration configuration)
        {
            var container = new Container();

            var scaffoldings = new IScaffolding[]
            {
                new ProcessingContextScaffolding(configuration),
                new ScriptProcessingScaffolding(),
                new ScriptExecutionScaffolding(),
                new SubjectProcessingScaffolding(configuration.FunctionHandlersProvider),
                new RootProcessingScaffolding(configuration.RootHandlerMappersProvider),
                new PathBuildingScaffolding(),
                new ConstantHelpersScaffolding(),
                new OperatorProcessingScaffolding(),
                new ProcessingSelectorsScaffolding(),
                new FunctionSubjectProcessingScaffolding(),

                // Script Parsing
                new ScriptParserScaffolding(),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            foreach (var extension in configuration.Extensions)
            {
                extension.Initialize(container);
            }

            // Additional processing (for path variable parts).
            SubjectParsingScaffolding.RegisterPathSubjectParsing(container);

            var scriptProcessor = container.GetInstance<IScriptProcessor>();

            var pathProcessor = container.GetInstance<IPathProcessor>();
            var pathSubjectToGraphPathConverter = container.GetInstance<IPathSubjectToGraphPathConverter>();

            var absolutePathSubjectProcessor = container.GetInstance<IAbsolutePathSubjectProcessor>();
            var relativePathSubjectProcessor = container.GetInstance<IRelativePathSubjectProcessor>();
            var rootedPathSubjectProcessor = container.GetInstance<IRootedPathSubjectProcessor>();

            container.GetInstance<IProcessingContext>().Initialize(pathSubjectToGraphPathConverter, absolutePathSubjectProcessor, relativePathSubjectProcessor, rootedPathSubjectProcessor, pathProcessor);

            return scriptProcessor;
        }
    }
}
