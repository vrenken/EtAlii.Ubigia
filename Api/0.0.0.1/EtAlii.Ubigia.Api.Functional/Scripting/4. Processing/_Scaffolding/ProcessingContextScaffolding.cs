namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.MicroContainer;

    internal class ProcessingContextScaffolding : IScaffolding
    {
        private readonly IScriptProcessorConfiguration _configuration;

        public ProcessingContextScaffolding(IScriptProcessorConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register<IProcessingContext, ProcessingContext>();
            container.Register(() => _configuration.LogicalContext);
            container.Register(() => _configuration.ScriptScope);
            container.Register(() => _configuration);
        }
    }
}
