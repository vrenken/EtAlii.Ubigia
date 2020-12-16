namespace EtAlii.xTechnology.Hosting
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
    public sealed class SystemSafeExecutionScope : IDisposable
    {
        private readonly Guid _uniqueId;
        public static TimeSpan DefaultTimeout { get; set; } = TimeSpan.FromMinutes(10);

        //edit by user "jitbit" - renamed private fields to "_"
        private readonly bool _hasHandle;
        private readonly Mutex _mutex;

        public SystemSafeExecutionScope(Guid uniqueId)
            : this(uniqueId, DefaultTimeout)
        {
        }

        public SystemSafeExecutionScope(Guid uniqueId, TimeSpan timeOut)
        {
            _uniqueId = uniqueId;
            _mutex = CreateMutex();

            WaitOne(timeOut, out _hasHandle);
        }

        private Mutex CreateMutex()
        {
            // unique id for global mutex - Global prefix means it is global to the machine
            var mutexName = $"Global\\{{{_uniqueId}}}";

            // edited by Jeremy Wiebe to add example of setting up security for multi-user usage
            // edited by 'Marc' to work also on localized systems (don't use just "Everyone")
            var securityIdentifier = (IdentityReference)new SecurityIdentifier(WellKnownSidType.WorldSid, null);
            var allowEveryoneRule = new MutexAccessRule(securityIdentifier, MutexRights.FullControl, AccessControlType.Allow);
            var securitySettings = new MutexSecurity();
            securitySettings.AddAccessRule(allowEveryoneRule);

            // edited by MasonGZhwiti to prevent race condition on security settings via VanNguyen
            var mutex = new Mutex(false, mutexName, out var createdNew);
            if (createdNew)
            {
                mutex.SetAccessControl(securitySettings);
            }

            AppDomain.CurrentDomain.ProcessExit += (_, _) => mutex.Close();
            return mutex;
        }

        private void WaitOne(TimeSpan timeout, out bool hasHandle)
        {
            try
            {
                // note, you may want to time out here instead of waiting forever
                // edited by acidzombie24
                // mutex.WaitOne(Timeout.Infinite, false)
                hasHandle = _mutex.WaitOne(timeout, false);
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

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_hasHandle)
                {
                    _mutex.ReleaseMutex();
                }
                _mutex.Close();
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~SystemSafeExecutionScope()
        {
            Dispose(false);
        }
    }
}
