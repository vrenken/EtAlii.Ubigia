﻿namespace EtAlii.xTechnology.Hosting
{
    public class StartSystemCommand : SystemCommandBase, IStartSystemCommand
    {
        public string Name { get; }

        public StartSystemCommand(ISystem system, string name)
            : base(system)
        {
            Name = name;
        }

        public void Execute()
        {
            System.Start();
        }

        protected override void OnSystemStateChanged(State state)
        {
            CanExecute = state != State.Running;
        }
    }
}