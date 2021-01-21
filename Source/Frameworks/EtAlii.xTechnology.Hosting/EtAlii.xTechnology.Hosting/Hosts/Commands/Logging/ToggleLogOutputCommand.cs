namespace EtAlii.xTechnology.Hosting
{
    internal class ToggleLogOutputCommand : HostCommandBase<IHost>, IToggleLogOutputCommand
    {
        public string Name => $"Host/Toggle log output";

        public ToggleLogOutputCommand(IHost host)
            : base(host)
        {
        }

        public void Execute()
        {
            Host.ShouldOutputLog = !Host.ShouldOutputLog;
        }

        protected override void OnHostStateChanged(State state)
        {
            CanExecute = state == State.Running;
        }
    }
}
