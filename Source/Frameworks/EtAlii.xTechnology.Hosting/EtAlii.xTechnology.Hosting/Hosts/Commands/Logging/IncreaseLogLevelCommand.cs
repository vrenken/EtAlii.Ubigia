// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    internal class IncreaseLogLevelCommand : HostCommandBase<IHost>, IIncreaseLogLevelCommand
    {
        public string Name => "Host/Increase log output";

        public IncreaseLogLevelCommand(IHost host)
            : base(host)
        {
        }

        public void Execute()
        {
            // if [Host.LogLevel ! = LogLevel.Trace]
            // [
            //     Host.LogLevel = Host.LogLevel - 1
            // ]
        }

        protected override void OnHostStateChanged(State state)
        {
            CanExecute = false; //state = = State.Running & & Host.LogLevel ! = LogLevel.Trace
        }

        protected override void OnHostStatusChanged(Status[] status)
        {
            CanExecute = false;//= Host.LogLevel ! = LogLevel.Trace
        }
    }
}
