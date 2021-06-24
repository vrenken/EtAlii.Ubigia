// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    public interface IEntryCacheContextProvider
    {
        IEntryContext Context { get; }
    }
}
