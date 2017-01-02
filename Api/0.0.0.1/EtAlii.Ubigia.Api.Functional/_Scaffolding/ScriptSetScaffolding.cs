namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    internal class ScriptSetScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IScriptsSet, ScriptsSet>();
        }
    }
}
