// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Local
{
    public class HostTestContext : HostTestContextBase<TestHost>
    {
        public HostTestContext(string hostConfigurationFile, string clientConfigurationFile)
            : base(hostConfigurationFile, clientConfigurationFile)
        {
            UseInProcessConnection = true;
        }
    }
}
