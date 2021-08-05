// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using EtAlii.xTechnology.MicroContainer;

    internal class DataConnectionScaffolding : IScaffolding
    {
        private readonly IDataConnectionOptions _options;

        public DataConnectionScaffolding(IDataConnectionOptions options)
        {
            _options = options;
        }

        public void Register(Container container)
        {
            container.Register(() => _options);
            container.Register(() => _options.ConfigurationRoot);
            container.Register<IDataConnection, DataConnection>();
        }
    }
}
