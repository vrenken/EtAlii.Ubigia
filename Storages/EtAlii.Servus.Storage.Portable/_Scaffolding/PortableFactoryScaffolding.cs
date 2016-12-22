namespace EtAlii.Servus.Storage.Portable
{
    using EtAlii.xTechnology.MicroContainer;
    using PCLStorage;

    public class PortableFactoryScaffolding : IScaffolding
    {
        private readonly IFolder _localStorage;

        internal PortableFactoryScaffolding(IFolder localStorage)
        {
            _localStorage = localStorage;
        }

        public void Register(Container container)
        {
            container.Register<IFolder>(() => _localStorage);
        }
    }
}
