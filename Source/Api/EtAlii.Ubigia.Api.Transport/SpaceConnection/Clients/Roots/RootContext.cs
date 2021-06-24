// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    public class RootContext : SpaceClientContextBase<IRootDataClient, IRootNotificationClient>, IRootContext
    {
        public RootContext(
            IRootNotificationClient notifications,
            IRootDataClient data)
            : base(notifications, data)
        {
        }
    }
}
