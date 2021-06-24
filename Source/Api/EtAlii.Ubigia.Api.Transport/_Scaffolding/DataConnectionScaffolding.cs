// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using EtAlii.xTechnology.MicroContainer;

    internal class DataConnectionScaffolding : IScaffolding
    {
        private readonly IDataConnectionConfiguration _configuration;

        public DataConnectionScaffolding(IDataConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register(() => _configuration);
            container.Register<IDataConnection, DataConnection>();
        }
    }
}
