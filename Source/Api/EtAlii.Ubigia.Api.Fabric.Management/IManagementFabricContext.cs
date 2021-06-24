// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Management
{
    using EtAlii.Ubigia.Api.Transport;

    public interface IManagementFabricContext
    {
        Storage Storage { get; }
        IStorageContext Storages { get; }
        IAccountContext Accounts { get; }
        ISpaceContext Spaces { get; }
    }
}
