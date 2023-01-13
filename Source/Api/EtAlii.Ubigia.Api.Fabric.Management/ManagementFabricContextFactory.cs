// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Management;

using EtAlii.Ubigia.Api.Transport.Management;
using EtAlii.xTechnology.MicroContainer;

public sealed class ManagementFabricContextFactory
{
    public IManagementFabricContext Create(IManagementConnection connection)
    {
        var container = new Container();
        container.Register<IManagementFabricContext, ManagementFabricContext>();
        container.Register(() => connection);

        //container.RegisterSingle<IStorageContext, StorageContext>()
        //container.RegisterSingle<IAccountContext, AccountContext>()
        //container.RegisterSingle<ISpaceContext, SpaceContext>()

        return container.GetInstance<IManagementFabricContext>();
    }
}
