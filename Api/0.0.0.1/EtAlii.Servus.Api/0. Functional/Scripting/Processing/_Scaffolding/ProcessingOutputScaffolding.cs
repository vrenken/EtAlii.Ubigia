namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    internal class ProcessingOutputScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<OutputAssigner>(Lifestyle.Singleton);
            container.Register<VariableAssigner>(Lifestyle.Singleton);
            container.Register<FunctionAssigner>(Lifestyle.Singleton);
        }
    }
}
