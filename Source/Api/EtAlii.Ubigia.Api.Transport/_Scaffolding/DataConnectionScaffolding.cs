// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using EtAlii.xTechnology.MicroContainer;

    internal class DataConnectionScaffolding : IScaffolding
    {
        private readonly DataConnectionOptions _options;

        public DataConnectionScaffolding(DataConnectionOptions options)
        {
            _options = options;
        }

        public void Register(IRegisterOnlyContainer container)
        {
            container.Register(() => _options.ConfigurationRoot);
            container.Register<IDataConnection>(() => new DataConnection(_options));
        }
    }
}
