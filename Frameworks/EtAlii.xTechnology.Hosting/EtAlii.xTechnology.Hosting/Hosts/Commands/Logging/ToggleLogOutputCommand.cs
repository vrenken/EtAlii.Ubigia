namespace EtAlii.xTechnology.Hosting
{
    internal class ToggleLogOutputCommand : HostCommandBase<IHost>, IToggleLogOutputCommand
    {
        private readonly IHostManager _manager;
        public string Name => $"Host/Toggle log output";

        public ToggleLogOutputCommand(IHost host, IHostManager manager)
            : base(host)
        {
            _manager = manager;
        }

        public void Execute()
        {
            _manager.ShouldOutputLog = !_manager.ShouldOutputLog;
        }

        protected override void OnHostStateChanged(State state)
        {
            CanExecute = state == State.Running;
        }
    }
}