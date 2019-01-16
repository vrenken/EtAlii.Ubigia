namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    public class StatusWriter : IStatusWriter
    {
        private readonly IMainDispatcherInvoker _dispatcherInvoker;

        public StatusWriter(IMainDispatcherInvoker dispatcherInvoker)
        {
            _dispatcherInvoker = dispatcherInvoker;
        }

        public void Write(IExecutionStatusProvider statusProvider, string message)
        {
            _dispatcherInvoker.SafeInvoke(() => statusProvider.ExecutionStatus.Add(message));

        }
    }
}