// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    internal class ContentContext : SpaceClientContextBase<IContentDataClient, IContentNotificationClient>, IContentContext
    {
        public ContentContext(
            IContentNotificationClient notifications,
            IContentDataClient data)
            : base(notifications, data)
        {
        }
    }
}
