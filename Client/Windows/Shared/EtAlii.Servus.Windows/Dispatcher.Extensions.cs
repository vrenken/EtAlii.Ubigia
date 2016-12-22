using System;
using System.Windows.Threading;

namespace EtAlii.Servus.Client.Windows.Shared
{
    public static class DispatcherExtensions
    {
        public static void SafeInvoke(this Dispatcher dispatcher, Action callBack, DispatcherPriority priority = DispatcherPriority.Normal)
        {
            if (!dispatcher.HasShutdownFinished && !dispatcher.HasShutdownStarted)
            {
                dispatcher.BeginInvoke(callBack, priority);
            }
        }
    }
}
