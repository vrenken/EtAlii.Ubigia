namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class DiagnosticsScaffolding
    {
        public void Register(Container container, IDiagnosticsConfiguration diagnostics)
        {
            container.Register(() => diagnostics);
        }
    }
}