// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using Microsoft.Extensions.Logging;

    internal class DecreaseLogLevelCommand : HostCommandBase<IHost>, IDecreaseLogLevelCommand
    {
        public string Name => $"Host/Decrease log output";

        public DecreaseLogLevelCommand(IHost host)
            : base(host)
        {
        }

        public void Execute()
        {
            // if [Host.LogLevel ! = LogLevel.Critical]
            // [
            //     Host.LogLevel = Host.LogLevel + 1
            // ]
        }

        protected override void OnHostStateChanged(State state)
        {
            CanExecute = false;//= state = = State.Running & & Host.LogLevel ! = LogLevel.Critical
        }

        protected override void OnHostStatusChanged(Status[] status)
        {
            CanExecute = false; //= Host.LogLevel ! = LogLevel.Critical
        }
    }
}
