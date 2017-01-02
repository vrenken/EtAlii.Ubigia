namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System;
    using System.Threading;
    using System.Windows.Threading;
    using EtAlii.Ubigia.Client.Windows.Shared;

    public class MainDispatcherInvoker
    {
        public void Invoke(Action action)
        {
            if (Dispatcher.CurrentDispatcher == App.Current.Dispatcher)
            {
                action();
            }
            else
            {
                App.Current.Dispatcher.Invoke(action);
            }
        }

        public void SafeInvoke(Action action)
        {
            if (App.Current != null)
            {
                App.Current.Dispatcher.SafeInvoke(action);
            }
        }

    }
}
