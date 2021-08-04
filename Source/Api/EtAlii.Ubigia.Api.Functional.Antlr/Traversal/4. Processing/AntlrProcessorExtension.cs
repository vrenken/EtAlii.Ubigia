// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Antlr.Traversal
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.xTechnology.MicroContainer;

    public class AntlrProcessorExtension : IScriptProcessorExtension
    {
        private readonly ITraversalProcessorOptions _options;

        public AntlrProcessorExtension(ITraversalProcessorOptions options)
        {
            _options = options;
        }

        public void Initialize(Container container)
        {
            new ScriptProcessingScaffolding(_options).Register(container);
            new ScriptExecutionPlanningScaffolding().Register(container);
            new SubjectProcessingScaffolding(_options.FunctionHandlersProvider).Register(container);
            new RootProcessingScaffolding(_options.RootHandlerMappersProvider).Register(container);
            new PathBuildingScaffolding().Register(container);
            new OperatorProcessingScaffolding().Register(container);
            new ProcessingSelectorsScaffolding().Register(container);
            new FunctionSubjectProcessingScaffolding().Register(container);
        }
    }
}
