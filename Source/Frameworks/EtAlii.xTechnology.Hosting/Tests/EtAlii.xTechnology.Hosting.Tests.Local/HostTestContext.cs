// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Local
{
    public class HostTestContext : HostTestContextBase<TestHost>
    {
        public HostTestContext(string configurationFile)
            : base(configurationFile)
        {
            UseInProcessConnection = true;
        }
    }
}
