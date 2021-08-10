// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using EtAlii.xTechnology.MicroContainer;

    internal class IdentifiersScaffolding : IScaffolding
    {
        public void Register(IRegisterOnlyContainer container)
        {
            container.Register<IIdentifierSet, IdentifierSet>();
        }
    }
}
