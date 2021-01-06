// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using EtAlii.xTechnology.MicroContainer;

    internal class TestSequencePartExecutionPlannerSelector
    {
        public static ISequencePartExecutionPlannerSelector Create()
        {
            var container = new Container();

            var configuration = new ScriptProcessorConfiguration();

            var scaffoldings = new IScaffolding[]
            {
                new ScriptProcessingScaffolding(configuration),
                new ScriptExecutionPlanningScaffolding(),
                new SubjectProcessingScaffolding(configuration.FunctionHandlersProvider),
                new RootProcessingScaffolding(configuration.RootHandlerMappersProvider),
                new PathBuildingScaffolding(),
                new LapaConstantParsingScaffolding(),
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
