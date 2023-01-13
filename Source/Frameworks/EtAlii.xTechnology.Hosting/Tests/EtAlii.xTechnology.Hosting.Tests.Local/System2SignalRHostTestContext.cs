// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Local;

public sealed class System2SignalRHostTestContext : LocalHostTestContext
{
    public System2SignalRHostTestContext() : base(ConfigurationFiles.HostSettingsSystems2VariantSignalR, ConfigurationFiles.ClientSettings)
    {

    }
}
