// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    public class StopSystemCommand : SystemCommandBase, IStopSystemCommand
    {
        public string Name { get; }

        public StopSystemCommand(ISystem system, string name)
            : base(system)
        {
            Name = name;
        }

        public void Execute()
        {
            System.Stop();
        }

        protected override void OnSystemStateChanged(State state)
        {
            CanExecute = state == State.Running;
        }
    }
}