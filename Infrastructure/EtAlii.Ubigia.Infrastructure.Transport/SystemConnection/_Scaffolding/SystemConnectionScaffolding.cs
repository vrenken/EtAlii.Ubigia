﻿namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.MicroContainer;

    public class SystemConnectionScaffolding : IScaffolding
    {
        private readonly ISystemConnectionConfiguration _configuration;

        public SystemConnectionScaffolding(ISystemConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register(() => _configuration);
            container.Register<ISystemConnection, SystemConnection>();
        }
    }
}
