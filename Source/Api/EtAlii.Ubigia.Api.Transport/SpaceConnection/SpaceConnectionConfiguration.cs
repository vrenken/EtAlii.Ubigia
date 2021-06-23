// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System;

    public class SpaceConnectionConfiguration : ConfigurationBase, ISpaceConnectionConfiguration
    {
        public ISpaceTransport Transport { get; private set; }

        public string Space { get; private set; }

        public ISpaceConnectionConfiguration Use(ISpaceTransport transport)
        {
            if (transport == null)
            {
                throw new ArgumentException("No account name specified", nameof(transport));
            }
            if (Transport != null)
            {
                throw new InvalidOperationException("A Transport has already been assigned to this SpaceConnectionConfiguration");
            }

            Transport = transport;
            return this;
        }
        
        public ISpaceConnectionConfiguration Use(string space)
        {
            if (string.IsNullOrWhiteSpace(space))
            {
                throw new ArgumentException("No space specified", nameof(space));
            }
            if (Space != null)
            {
                throw new InvalidOperationException("A space has already been assigned to this SpaceConnectionConfiguration");
            }

            Space = space;
            return this;
        }
    }
}