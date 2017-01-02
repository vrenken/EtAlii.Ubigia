namespace EtAlii.Servus.Api.Transport
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    internal class SpaceConnectionDebuggingScaffolding : IScaffolding
    {
        public SpaceConnectionDebuggingScaffolding()
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
