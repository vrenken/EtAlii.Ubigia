// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    internal class ScriptProcessorFactory : IScriptProcessorFactory
    {
        public ScriptProcessorFactory()
        {
        }

        public IScriptProcessor Create(IScriptProcessorConfiguration configuration)
        {
            var container = new Container();

            var scaffoldings = new IScaffolding[]
            {
                new ProcessingContextScaffolding(configuration),
                new ScriptProcessingScaffolding(),
                new ScriptExecutionScaffolding(),
                new SubjectProcessingScaffolding(),
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
            container.GetInstance<IProcessingContext>().Initialize(pathSubjectToGraphPathConverter, pathProcessor);

            return scriptProcessor;
        }
    }
}
