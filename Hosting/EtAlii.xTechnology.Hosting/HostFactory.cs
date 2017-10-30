namespace EtAlii.xTechnology.Hosting
{
    using System;
    using EtAlii.xTechnology.MicroContainer;

    public class HostFactory<THost>
        where THost : class, IHost
    {
        public IHost Create(IHostConfiguration configuration)
        {
            var container = new Container();

            var scaffoldings = new IScaffolding[]
            {
                new HostScaffolding<THost>(configuration),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            foreach (var extension in configuration.Extensions)
            {
                extension.Register(container);
            }

            return container.GetInstance<IHost>();
        }
    }
}
