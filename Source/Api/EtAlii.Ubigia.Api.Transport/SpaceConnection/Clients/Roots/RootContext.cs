// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport;

public sealed class RootContext : SpaceClientContextBase<IRootDataClient>, IRootContext
{
    public RootContext(
        IRootDataClient data)
        : base(data)
    {
    }
}
