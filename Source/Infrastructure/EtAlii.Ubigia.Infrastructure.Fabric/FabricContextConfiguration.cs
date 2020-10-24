﻿namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Persistence;

    public class FabricContextConfiguration : Configuration
    {
        public IStorage Storage { get; private set; }

        public FabricContextConfiguration Use(IStorage storage)
        {
            Storage = storage ?? throw new ArgumentException(nameof(storage));

            return this;
        }
    }
}