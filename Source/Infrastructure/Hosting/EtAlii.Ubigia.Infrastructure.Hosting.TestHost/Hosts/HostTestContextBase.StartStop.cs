// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost
{
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Hosting;

    public abstract partial class HostTestContextBase
    {
        public override async Task Start(PortRange portRange)
        {
            await base.Start(portRange).ConfigureAwait(false);

            Infrastructure = Host.Infrastructure;

            var systemAccount = await Infrastructure.Accounts.Get("System").ConfigureAwait(false);
            SystemAccountName = systemAccount.Name;
            SystemAccountPassword = systemAccount.Password;

            var adminAccount = await Infrastructure.Accounts.Get("Administrator").ConfigureAwait(false);
            AdminAccountName = adminAccount.Name;
            AdminAccountPassword = adminAccount.Password;

            // Create test user account and use this instead of the admin account.
            // More details can be found in the Github issue below:
            // https://github.com/vrenken/EtAlii.Ubigia/issues/92
            TestAccountName = adminAccount.Name;
            TestAccountPassword = adminAccount.Password;
        }

        public override async Task Stop()
        {
            await base.Stop().ConfigureAwait(false);

            Infrastructure = null;

            SystemAccountName = null;
            SystemAccountPassword = null;
            TestAccountName = null;
            TestAccountPassword = null;
        }
    }
}
