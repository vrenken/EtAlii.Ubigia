namespace EtAlii.Servus.Api.Management
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    internal class StorageConnectionDebuggingScaffolding : IScaffolding
    {
        public StorageConnectionDebuggingScaffolding()
        {
        }

        public void Register(Container container)
        {
            var diagnostics = container.GetInstance<IDiagnosticsConfiguration>();

            if (diagnostics.EnableDebugging) // diagnostics is enabled
            {
            }
        }
    }
}
