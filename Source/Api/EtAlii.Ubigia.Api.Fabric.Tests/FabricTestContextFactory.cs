// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Tests
{
    using EtAlii.Ubigia.Api.Transport.Tests;
    using EtAlii.xTechnology.MicroContainer;

    public class FabricTestContextFactory
    {
        public IFabricTestContext Create()
        {
            var container = new Container();

            container.Register<IFabricTestContext, FabricTestContext>();
            container.Register(() => new TransportTestContext().Create());

            return container.GetInstance<IFabricTestContext>();
        }
    }
}
