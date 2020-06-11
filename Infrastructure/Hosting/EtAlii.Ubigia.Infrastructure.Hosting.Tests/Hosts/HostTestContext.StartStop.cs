namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using System.Threading.Tasks;

    public abstract partial class HostTestContextBase<TTestHost> 
        where TTestHost : class, IInfrastructureTestHostBase
    {
        // protected abstract void StartInternal(bool useRandomPorts);
        // protected abstract void StopInternal();

        //private static readonly System.Random _random = new Random()

        public override async Task Start()
        {
            await base.Start();

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
            await base.Stop();
            
            Infrastructure = null;

            SystemAccountName = null;
            SystemAccountPassword = null;
            TestAccountName = null;
            TestAccountPassword = null;
        }
    }
}