namespace EtAlii.Servus.Storage.Ntfs
{
    using EtAlii.xTechnology.MicroContainer;

    public class NtfsFactoryScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<INtfsStorageConfiguration>(() => (INtfsStorageConfiguration)container.GetInstance<IStorageConfiguration>());
        }
    }
}
