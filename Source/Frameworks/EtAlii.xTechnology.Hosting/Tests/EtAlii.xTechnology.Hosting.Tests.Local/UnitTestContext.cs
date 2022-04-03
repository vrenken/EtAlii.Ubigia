// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Local
{
    using System.Threading.Tasks;
    using Xunit;

    public class UnitTestContext<TLocalHostTestContext> : IAsyncLifetime
        where TLocalHostTestContext : LocalHostTestContext, new()
    {
        public LocalHostTestContext Host { get; private set; }

        public async Task InitializeAsync()
        {
            Host = new TLocalHostTestContext();
            await Host
                .Start(UnitTestSettings.NetworkPortRange)
                .ConfigureAwait(false);
        }

        public async Task DisposeAsync()
        {
            await Host
                .Stop()
                .ConfigureAwait(false);

            Host = null;
        }
    }
}
