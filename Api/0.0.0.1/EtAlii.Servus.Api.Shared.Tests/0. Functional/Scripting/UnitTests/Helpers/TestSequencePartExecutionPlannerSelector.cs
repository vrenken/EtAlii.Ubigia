// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Servus.Api.Functional.Tests
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

            // Additional processing (for path variable parts).
            SubjectParsingScaffolding.RegisterPathSubjectParsing(container);

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            return container.GetInstance<ISequencePartExecutionPlannerSelector>();
        }
    }
}