// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost
{
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Hosting;

    // ReSharper disable once UnusedTypeParameter
    public abstract partial class HostTestContextBase<TTestHost>
    {
        public override async Task Start(PortRange portRange)
        {
            await base.Start(portRange).ConfigureAwait(false);

            Infrastructure = Host.Infrastructure;
            
            var systemAccount = Infrastructure.Accounts.Get("System");
            SystemAccountName = systemAccount.Name;
            SystemAccountPassword = systemAccount.Password;

            var adminAccount = Infrastructure.Accounts.Get("Administrator");
            AdminAccountName = adminAccount.Name;
            AdminAccountPassword = adminAccount.Password;

            // TODO: Create test user account and use this instead of the admin account.
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