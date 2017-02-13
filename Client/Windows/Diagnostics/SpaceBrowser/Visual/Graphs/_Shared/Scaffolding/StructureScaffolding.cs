namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using SimpleInjector;

    public class StructureScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IMainDispatcherInvoker, MainDispatcherInvoker>(Lifestyle.Singleton);
        }
    }
}