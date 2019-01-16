namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
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