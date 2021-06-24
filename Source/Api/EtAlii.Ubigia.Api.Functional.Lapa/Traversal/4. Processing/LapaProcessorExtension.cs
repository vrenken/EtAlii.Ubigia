// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;

    public class LapaProcessorExtension : IScriptProcessorExtension
    {
        private readonly ITraversalProcessorConfiguration _configuration;

        public LapaProcessorExtension(ITraversalProcessorConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Initialize(Container container)
        {
            // Processing.
            new ScriptProcessingScaffolding(_configuration).Register(container);
            new ScriptExecutionPlanningScaffolding().Register(container);
            new SubjectProcessingScaffolding(_configuration.FunctionHandlersProvider).Register(container);
            new RootProcessingScaffolding(_configuration.RootHandlerMappersProvider).Register(container);
            new PathBuildingScaffolding().Register(container);
            new OperatorProcessingScaffolding().Register(container);
            new ProcessingSelectorsScaffolding().Register(container);
            new FunctionSubjectProcessingScaffolding().Register(container);
        }
    }
}
