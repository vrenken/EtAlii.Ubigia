// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
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
        private static TimeSpan DefaultTimeout { get; set; } = TimeSpan.FromMinutes(10);

        //edit by user "jitbit" - renamed private fields to "_"
        private readonly bool _hasHandle;
        private readonly Mutex _mutex;

        public SystemSafeExecutionScope(Guid uniqueId)
            : this(uniqueId, DefaultTimeout)
        {
        }

        private SystemSafeExecutionScope(Guid uniqueId, TimeSpan timeOut)
        {
            _uniqueId = uniqueId;
            _mutex = CreateMutex();

            WaitOne(timeOut, out _hasHandle);
        }

        private Mutex CreateMutex()
        {
            // unique id for global mutex - Global prefix means it is global to the machine
            var mutexName = $"Global\\{{{_uniqueId}}}";

            // edited by MasonGZhwiti to prevent race condition on security settings via VanNguyen
            var mutex = new Mutex(false, mutexName);

            AppDomain.CurrentDomain.ProcessExit += (_, _) => Dispose();
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
                {
                    throw new TimeoutException("Timeout waiting for exclusive access");
                }
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
                    try
                    {
                        _mutex.ReleaseMutex();
                    }
                    catch
                    {
                        // This release throws when there is nothing to release.
                    }
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
