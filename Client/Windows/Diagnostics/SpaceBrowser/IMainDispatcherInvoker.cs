namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System;

    public interface IMainDispatcherInvoker
    {
        void Invoke(Action action);
        void SafeInvoke(Action action);
    }
}