namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;

    public class AntlrProcessorExtension : IScriptProcessorExtension
    {
        private readonly ITraversalProcessorConfiguration _configuration;

        public AntlrProcessorExtension(ITraversalProcessorConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Initialize(Container container)
        {
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
