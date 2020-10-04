﻿namespace EtAlii.Ubigia.Api.Fabric.Management
{
    using EtAlii.Ubigia.Api.Transport;

    public class ManagementFabricContext : IManagementFabricContext
    {
        public Storage Storage { get; }

        public IStorageContext Storages { get; }

        public IAccountContext Accounts { get; }

        public ISpaceContext Spaces { get; }

        public ManagementFabricContext(
            Storage storage, 
            IStorageContext storages, 
            IAccountContext accounts, 
            ISpaceContext spaces)
        {
            Storage = storage;
            Storages = storages;
            Accounts = accounts;
            Spaces = spaces;
        }
    }
}
