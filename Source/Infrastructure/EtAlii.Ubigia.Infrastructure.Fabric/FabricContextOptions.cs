// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using EtAlii.Ubigia.Persistence;

    public class FabricContextOptions : ConfigurationBase
    {
        public IStorage Storage { get; private set; }

        public FabricContextOptions Use(IStorage storage)
        {
            Storage = storage ?? throw new ArgumentException("No storage specified", nameof(storage));

            return this;
        }
    }
}
