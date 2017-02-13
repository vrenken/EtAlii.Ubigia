namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.xTechnology.MicroContainer;

    public class StructureScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IMainDispatcherInvoker, MainDispatcherInvoker>();
        }
    }
}