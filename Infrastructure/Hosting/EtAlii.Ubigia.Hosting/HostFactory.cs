namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using System;
    using System.Collections.Generic;
    using EtAlii.xTechnology.MicroContainer;

    public class HostFactory : IHostFactory
    {
        public IHost Create(IHostConfiguration configuration)
        {
            if (configuration.Infrastructure == null)
            {
                throw new NotSupportedException("A Infrastructure is required to construct a Host instance");
            }
            if (configuration.Storage == null)
            {
                throw new NotSupportedException("A Storage is required to construct a Host instance");
            }

            var container = new Container();

            var scaffoldings = new List<IScaffolding>(new IScaffolding[]
            {
                new HostScaffolding(configuration), 

                //new HostDiagnosticsScaffolding(diagnostics), 
                //new HostProfilingScaffolding(diagnostics), 
                //new HostLoggingScaffolding(diagnostics), 
                //new HostDebuggingScaffolding(diagnostics), 
            });

            foreach(var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            foreach (var extension in configuration.Extensions)
            {
                extension.Initialize(container);
            }

            return configuration.GetHost(container);
        }
    }
}