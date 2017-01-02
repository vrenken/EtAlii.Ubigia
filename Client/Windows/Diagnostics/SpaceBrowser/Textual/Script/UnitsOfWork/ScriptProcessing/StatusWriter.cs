namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    public class StatusWriter : IStatusWriter
    {
        private readonly MainDispatcherInvoker _dispatcherInvoker;

        public StatusWriter(MainDispatcherInvoker dispatcherInvoker)
        {
            _dispatcherInvoker = dispatcherInvoker;
        }

        public void Write(ScriptViewModel viewModel, string message)
        {
            _dispatcherInvoker.SafeInvoke(() => viewModel.ExecutionStatus.Add(message));

        }
    }
}