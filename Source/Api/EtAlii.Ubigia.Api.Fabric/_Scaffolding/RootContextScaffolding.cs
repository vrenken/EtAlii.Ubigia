// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using EtAlii.xTechnology.MicroContainer;

    internal class RootContextScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IRootContext, RootContext>();
        }
    }
}