namespace EtAlii.xTechnology.Networking
{
    using System;
    using System.Security.AccessControl;
    using System.Security.Principal;
    using System.Threading;

    /// <summary>
    /// A helper class that allows code to run with a system-wide guarantee on exclusivity.
    ///
    /// The code is based on the splendid work done by the people mentioned in both the code and on the
    /// StackOverflow page found below:
    /// https://stackoverflow.com/questions/229565/what-is-a-good-pattern-for-using-a-global-mutex-in-c 
    /// </summary>
    public static class SystemExclusiveExecution
    {
        private static readonly Guid HostId = Guid.Parse("B90F76E7-C278-4D52-BD89-A6D6521D6B43"); 
        private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(120);

        public static void Run(Action action) => Run(action, DefaultTimeout);
        
        // ReSharper disable once MemberCanBePrivate.Global
        public static void Run(Action action, TimeSpan timeout)
        {
            var mutex = CreateMutex();
            // edited by acidzombie24
            var hasHandle = false;
            try
            {
                WaitOne(mutex, timeout, out hasHandle);
                
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

                
        public static TResult Run<T1, TResult>(Func<T1, TResult> function, T1 t1) => Run(function, t1, DefaultTimeout);

        // ReSharper disable once MemberCanBePrivate.Global
        public static TResult Run<T1, TResult>(Func<T1, TResult> function, T1 t1, TimeSpan timeout)
        {
            TResult result;

            var mutex = CreateMutex();
            // edited by acidzombie24
            var hasHandle = false;
            try
            {
                WaitOne(mutex, timeout, out hasHandle);
                
                // Perform your work here.
                result = function(t1);
            }
            finally
            {
                // edited by acidzombie24, added if statement
                if(hasHandle)
                    mutex.ReleaseMutex();
            }

            return result;
        }
        
        public static TResult Run<T1, T2, TResult>(Func<T1, T2, TResult> function, T1 t1, T2 t2) => Run(function, t1, t2, DefaultTimeout);

        // ReSharper disable once MemberCanBePrivate.Global
        public static TResult Run<T1, T2, TResult>(Func<T1, T2, TResult> function, T1 t1, T2 t2, TimeSpan timeout)
        {
            TResult result;

            var mutex = CreateMutex();
            // edited by acidzombie24
            var hasHandle = false;
            try
            {
                WaitOne(mutex, timeout, out hasHandle);
                
                // Perform your work here.
                result = function(t1, t2);
            }
            finally
            {
                // edited by acidzombie24, added if statement
                if(hasHandle)
                    mutex.ReleaseMutex();
            }

            return result;
        }

        public static TResult Run<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> function, T1 t1, T2 t2, T3 t3) => Run(function, t1, t2, t3, DefaultTimeout);

        // ReSharper disable once MemberCanBePrivate.Global
        public static TResult Run<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> function, T1 t1, T2 t2, T3 t3, TimeSpan timeout)
        {
            TResult result;

            var mutex = CreateMutex();
            // edited by acidzombie24
            var hasHandle = false;
            try
            {
                WaitOne(mutex, timeout, out hasHandle);
                
                // Perform your work here.
                result = function(t1, t2, t3);
            }
            finally
            {
                // edited by acidzombie24, added if statement
                if(hasHandle)
                    mutex.ReleaseMutex();
            }

            return result;
        }

        private static Mutex CreateMutex()
        {
            // get application GUID as defined in AssemblyInfo.cs
            //var appGuid = ((GuidAttribute)Assembly.GetExecutingAssembly()
            //    .GetCustomAttributes(typeof(GuidAttribute), false)
            //    .GetValue(0)).Value.ToString()
            var appGuid = HostId;
            
            // unique id for global mutex - Global prefix means it is global to the machine
            var mutexId = $"Global\\{{{appGuid}}}";
                
            // edited by Jeremy Wiebe to add example of setting up security for multi-user usage
            // edited by 'Marc' to work also on localized systems (don't use just "Everyone") 
            var securityIdentifier = (IdentityReference)new SecurityIdentifier(WellKnownSidType.WorldSid, null);
            var allowEveryoneRule = new MutexAccessRule(securityIdentifier, MutexRights.FullControl, AccessControlType.Allow);
            var securitySettings = new MutexSecurity();
            securitySettings.AddAccessRule(allowEveryoneRule);
        
            // edited by MasonGZhwiti to prevent race condition on security settings via VanNguyen
            using var mutex = new Mutex(false, mutexId, out var createdNew);
            if (createdNew)
            {
                mutex.SetAccessControl(securitySettings);
            }
            return mutex;
        }


        private static void WaitOne(Mutex mutex, TimeSpan timeout, out bool hasHandle)
        {
            try
            {
                // note, you may want to time out here instead of waiting forever
                // edited by acidzombie24
                // mutex.WaitOne(Timeout.Infinite, false)
                hasHandle = mutex.WaitOne(timeout, false);
                if (!hasHandle)
                    throw new TimeoutException("Timeout waiting for exclusive access");
            }
            catch (AbandonedMutexException)
            {
                // Log the fact that the mutex was abandoned in another process,
                // it will still get acquired
                hasHandle = true;
            }

        }
    }
}
