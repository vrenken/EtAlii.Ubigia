// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Local
{
    public class System2GrpcHostTestContext : LocalHostTestContext
    {
        public System2GrpcHostTestContext() : base(ConfigurationFiles.HostSettingsSystems2VariantGrpc, ConfigurationFiles.ClientSettings)
        {
            
        }
    }
}
