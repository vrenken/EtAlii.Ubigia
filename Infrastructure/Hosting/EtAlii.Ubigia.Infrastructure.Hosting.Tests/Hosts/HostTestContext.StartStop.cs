namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    public abstract partial class HostTestContextBase
    {
        protected abstract void StartInternal(bool useRandomPorts);
        protected abstract void StopInternal();

        public void Start() 
        {
            // We need to start each test hosting one at a time. 
            // Reason is that this is the only way to make sure that the ports aren't reused.
            HostMutex.ExecuteExclusive(() => StartInternal(true));
            
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
        
        public void Stop()
        {
            StopInternal();
            
            Infrastructure = null;

            SystemAccountName = null;
            SystemAccountPassword = null;
            TestAccountName = null;
            TestAccountPassword = null;
        }
    }
}