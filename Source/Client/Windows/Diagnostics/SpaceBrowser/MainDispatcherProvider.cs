namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Windows.Threading;

    public class MainDispatcherInvoker : IMainDispatcherInvoker
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
