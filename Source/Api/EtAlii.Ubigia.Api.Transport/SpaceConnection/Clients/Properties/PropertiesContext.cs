// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    internal sealed class PropertiesContext : SpaceClientContextBase<IPropertiesDataClient>, IPropertiesContext
    {
        public PropertiesContext(
            IPropertiesDataClient data)
            : base(data)
        {
        }
    }
}
