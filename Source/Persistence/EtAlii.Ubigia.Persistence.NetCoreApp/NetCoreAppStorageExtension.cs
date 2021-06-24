// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.NetCoreApp
{
    using EtAlii.xTechnology.MicroContainer;

    public class NetCoreAppStorageExtension : IStorageExtension
    {
        private readonly string _baseFolder;

        public NetCoreAppStorageExtension(string baseFolder)
        {
            _baseFolder = baseFolder;
        }

        public void Initialize(Container container)
        {
            var scaffoldings = new IScaffolding[]
            {
                new SharedFactoryScaffolding<NetCoreAppStorageSerializer, NetCoreAppFolderManager, NetCoreAppFileManager, NetCoreAppPathBuilder, DefaultContainerProvider>(),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
            container.RegisterInitializer<IPathBuilder>(pb => ((NetCoreAppPathBuilder)pb).Initialize(_baseFolder));
        }
    }
}
