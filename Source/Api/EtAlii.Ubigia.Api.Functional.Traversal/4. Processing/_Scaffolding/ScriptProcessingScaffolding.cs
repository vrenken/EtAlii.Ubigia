namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;

    internal class ScriptProcessingScaffolding : IScaffolding
    {
        private readonly IScriptProcessorConfiguration _configuration;

        public ScriptProcessingScaffolding(IScriptProcessorConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register<IScriptProcessor, ScriptProcessor>();

            container.Register<IScriptProcessingContext, ScriptProcessingContext>();
            container.Register(() => _configuration.LogicalContext);
            container.Register(() => _configuration.ScriptScope);
            container.Register(() => _configuration);
        }
    }
}
