// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    using EtAlii.xTechnology.MicroContainer;

    public class PropertiesScaffolding : IScaffolding
    {
        public void Register(IRegisterOnlyContainer container)
        {
            container.Register<IPropertiesStorage, PropertiesStorage>();
            container.Register<IPropertiesStorer, PropertiesStorer>();
            container.Register<IPropertiesRetriever, PropertiesRetriever>();
        }
    }
}
