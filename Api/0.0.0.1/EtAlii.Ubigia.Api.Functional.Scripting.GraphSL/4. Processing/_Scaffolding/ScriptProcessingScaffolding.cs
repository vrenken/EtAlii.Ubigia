namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    internal class ScriptProcessingScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IScriptProcessor, ScriptProcessor>();
        }
    }
}
