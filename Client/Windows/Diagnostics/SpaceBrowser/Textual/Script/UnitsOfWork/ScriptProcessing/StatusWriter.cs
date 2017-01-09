namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    public class StatusWriter : IStatusWriter
    {
        private readonly IMainDispatcherInvoker _dispatcherInvoker;

        public StatusWriter(IMainDispatcherInvoker dispatcherInvoker)
        {
            _dispatcherInvoker = dispatcherInvoker;
        }

        public void Write(IScriptViewModel viewModel, string message)
        {
            _dispatcherInvoker.SafeInvoke(() => viewModel.ExecutionStatus.Add(message));

        }
    }
}