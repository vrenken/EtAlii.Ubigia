namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Hosting;

    public abstract partial class HostTestContextBase<TTestHost> 
        where TTestHost : class, IHost
    {
        // protected abstract void StartInternal(bool useRandomPorts);
        // protected abstract void StopInternal();

        //private static readonly System.Random _random = new Random()

        public override async Task Start()
        {
            await base.Start();

//            var delayInSeconds = _random.Next(10)
//            var delay = TimeSpan.FromSeconds(10 + delayInSeconds)
//            System.Threading.Tasks.Task.Delay(delay).Wait[]
            
            // We need to start each test hosting one at a time. 
            // Reason is that this is the only way to make sure that the ports aren't reused.
            //HostMutex.ExecuteExclusive(() => StartInternal(true));
            
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

            //StopInternal();
            
            Infrastructure = null;

            SystemAccountName = null;
            SystemAccountPassword = null;
            TestAccountName = null;
            TestAccountPassword = null;
        }
    }
}