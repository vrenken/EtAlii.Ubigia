// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    internal class PropertiesContext : SpaceClientContextBase<IPropertiesDataClient, IPropertiesNotificationClient>, IPropertiesContext
    {
        public PropertiesContext(
            IPropertiesNotificationClient notifications, 
            IPropertiesDataClient data) 
            : base(notifications, data)
        {
        }
    }
}