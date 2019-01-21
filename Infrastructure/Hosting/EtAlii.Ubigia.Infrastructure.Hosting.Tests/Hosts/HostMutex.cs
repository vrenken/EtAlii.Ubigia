namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using System;
    using System.Security.AccessControl;    //MutexAccessRule
    using System.Security.Principal;
    using System.Threading;

    public class HostMutex
    {
        // Code originated from:
        // https://stackoverflow.com/questions/229565/what-is-a-good-pattern-for-using-a-global-mutex-in-c
        private static readonly Guid HostId = Guid.Parse("2456c6ff-3980-4893-9751-353e281174e8");
        private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(120);
        
        public static void ExecuteExclusive(Action action)
        {
            // get application GUID as defined in AssemblyInfo.cs
            var appGuid = HostId;
            
            // unique id for global mutex - Global prefix means it is global to the machine
            var mutexId = string.Format( "Global\\{{{0}}}", appGuid );
                
            // edited by Jeremy Wiebe to add example of setting up security for multi-user usage
            // edited by 'Marc' to work also on localized systems (don't use just "Everyone") 
            var securityIdentifier = (IdentityReference)new SecurityIdentifier(WellKnownSidType.WorldSid, null);
            var allowEveryoneRule = new MutexAccessRule(securityIdentifier, MutexRights.FullControl, AccessControlType.Allow);
            var securitySettings = new MutexSecurity();
            securitySettings.AddAccessRule(allowEveryoneRule);
        
            // edited by MasonGZhwiti to prevent race condition on security settings via VanNguyen
            using (var mutex = new Mutex(false, mutexId, out bool createdNew))
            {
                if (createdNew)
                {
                    mutex.SetAccessControl(securitySettings);
                }

                // edited by acidzombie24
                var hasHandle = false;
                try
                {
                    try
                    {
                        // note, you may want to time out here instead of waiting forever
                        // edited by acidzombie24
                        hasHandle = mutex.WaitOne(Timeout, false);
                        if (hasHandle == false)
                            throw new TimeoutException("Timeout waiting for exclusive access");
                    }
                    catch (AbandonedMutexException)
                    {
                        // Log the fact that the mutex was abandoned in another process,
                        // it will still get acquired
                        hasHandle = true;
                    }
        
                    // Perform your work here.
                    action();
                }
                finally
                {
                    // edited by acidzombie24, added if statement
                    if(hasHandle)
                        mutex.ReleaseMutex();
                }
            }
        }
    }
}
