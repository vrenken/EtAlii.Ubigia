// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    internal class TestSequencePartExecutionPlannerSelector
    {
        public static ISequencePartExecutionPlannerSelector Create(IConfigurationRoot configurationRoot)
        {
            var container = new Container();

            var options = new TraversalProcessorOptions(configurationRoot);

            var scaffoldings = new IScaffolding[]
            {
                new ScriptProcessingScaffolding(options),
                new ScriptExecutionPlanningScaffolding(),
                new SubjectProcessingScaffolding(options.FunctionHandlersProvider),
                new RootProcessingScaffolding(options.RootHandlerMappersProvider),
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

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            return container.GetInstance<ISequencePartExecutionPlannerSelector>();
        }
    }
}
