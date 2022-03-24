// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Local
{
    public sealed class System2RestHostTestContext : LocalHostTestContext
    {
        public System2RestHostTestContext() : base(ConfigurationFiles.HostSettingsSystems2VariantRest, ConfigurationFiles.ClientSettings)
        {

        }
    }
}
