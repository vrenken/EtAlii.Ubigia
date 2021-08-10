// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Azure
{
    using EtAlii.xTechnology.MicroContainer;

    public class AzureStorageExtension : IExtension
    {
        public void Initialize(IRegisterOnlyContainer container)
        {
            var scaffoldings = new IScaffolding[]
            {
                new AzureFactoryScaffolding(),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}
