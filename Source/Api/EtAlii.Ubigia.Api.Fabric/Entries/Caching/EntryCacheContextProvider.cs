// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    internal class EntryCacheContextProvider : IEntryCacheContextProvider
    {
        public IEntryContext Context { get; }

        public EntryCacheContextProvider(IEntryContext context)
        {
            Context = context;
        }
    }
}
