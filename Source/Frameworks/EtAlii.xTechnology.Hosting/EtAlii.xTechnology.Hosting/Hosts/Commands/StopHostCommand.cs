namespace EtAlii.xTechnology.Hosting
{
    internal class StopHostCommand : HostCommandBase, IStopHostCommand
    {
        public string Name => "Host/Stop";

        public StopHostCommand(IHost host)
            : base(host)
        {
        }

        public void Execute()
        {
            Host.Stop();
        }

        protected override void OnHostStateChanged(State state)
        {
            CanExecute = state == State.Running;
        }
    }
}