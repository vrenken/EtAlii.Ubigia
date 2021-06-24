// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using EtAlii.xTechnology.MicroContainer;

    internal class PropertiesScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IPropertiesSet, PropertiesSet>();

            container.Register<IPropertiesGetter, PropertiesGetter>();
            container.Register<IPropertiesStorer, PropertiesStorer>();
        }
    }
}