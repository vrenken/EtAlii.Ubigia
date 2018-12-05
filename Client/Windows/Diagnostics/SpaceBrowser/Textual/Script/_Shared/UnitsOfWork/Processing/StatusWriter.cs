namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    public class StatusWriter : IStatusWriter
    {
        private readonly IMainDispatcherInvoker _dispatcherInvoker;

        public StatusWriter(IMainDispatcherInvoker dispatcherInvoker)
        {
            _dispatcherInvoker = dispatcherInvoker;
        }

        public void Write(IGraphScriptLanguageViewModel viewModel, string message)
        {
            _dispatcherInvoker.SafeInvoke(() => viewModel.ExecutionStatus.Add(message));

        }
    }
}