// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.MicroContainer;

    public class SystemConnectionScaffolding : IScaffolding
    {
        private readonly ISystemConnectionOptions _options;

        public SystemConnectionScaffolding(ISystemConnectionOptions options)
        {
            _options = options;
        }

        /// <inheritdoc />
        public void Register(IRegisterOnlyContainer container)
        {
            container.Register(() => _options);
            container.Register<ISystemConnection, SystemConnection>();
        }
    }
}
