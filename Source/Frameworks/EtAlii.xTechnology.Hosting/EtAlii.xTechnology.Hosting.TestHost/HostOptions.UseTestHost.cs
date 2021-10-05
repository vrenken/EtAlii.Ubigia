// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using EtAlii.xTechnology.MicroContainer;

    public static class HostOptionsUseTestHostExtension
    {
        public static HostOptions UseTestHost(this HostOptions options, Func<HostOptions, ITestHost> hostFactory)
        {
            return options
                .Use(new IExtension[] { new TestHostExtension(options, hostFactory) })
                .UseWrapper(true);
        }
    }
}
