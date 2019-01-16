namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;

    public interface IMainDispatcherInvoker
    {
        void Invoke(Action action);
        void SafeInvoke(Action action);
    }
}