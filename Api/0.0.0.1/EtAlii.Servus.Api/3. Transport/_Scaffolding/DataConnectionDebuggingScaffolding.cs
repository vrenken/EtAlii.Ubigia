namespace EtAlii.Servus.Api.Transport
{
    using EtAlii.xTechnology.MicroContainer;

    internal class DataConnectionDebuggingScaffolding : IScaffolding
    {
        public DataConnectionDebuggingScaffolding()
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
