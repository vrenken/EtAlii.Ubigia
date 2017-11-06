using System;

namespace EtAlii.xTechnology.Hosting
{
    public interface IHostCommand
    {
        string Name { get; }
        void Execute();
        bool CanExecute { get; }

        event EventHandler CanExecuteChanged;
        void Initialize(IHost host);
    }
}
