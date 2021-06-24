// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Azure
{
    using EtAlii.xTechnology.MicroContainer;

    public class AzureStorageExtension : IStorageExtension
    {
        public void Initialize(Container container)
        {
            var scaffoldings = new IScaffolding[]
            {
                new SharedFactoryScaffolding<AzureStorageSerializer, AzureFolderManager, AzureFileManager, AzurePathBuilder, DefaultContainerProvider>(),
                new AzureFactoryScaffolding(),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}
