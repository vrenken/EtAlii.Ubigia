// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    public sealed class EntryContext : SpaceClientContextBase<IEntryDataClient>, IEntryContext
    {
        public EntryContext(
            IEntryDataClient data)
            : base(data)
        {
        }
    }
}
