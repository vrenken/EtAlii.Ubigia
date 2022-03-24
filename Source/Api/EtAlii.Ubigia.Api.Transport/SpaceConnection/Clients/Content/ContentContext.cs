// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    internal sealed class ContentContext : SpaceClientContextBase<IContentDataClient>, IContentContext
    {
        public ContentContext(
            IContentDataClient data)
            : base(data)
        {
        }
    }
}
