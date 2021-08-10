// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.NetCoreApp
{
    using EtAlii.xTechnology.MicroContainer;

    public class NetCoreAppStorageExtension : IExtension
    {
        private readonly string _baseFolder;

        public NetCoreAppStorageExtension(string baseFolder)
        {
            _baseFolder = baseFolder;
        }

        public void Initialize(IRegisterOnlyContainer container)
        {
            var scaffoldings = new IScaffolding[]
            {
                new NetCoreAppFactoryScaffolding()
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
            container.RegisterInitializer<IPathBuilder>(pb => ((NetCoreAppPathBuilder)pb).Initialize(_baseFolder));
        }
    }
}
