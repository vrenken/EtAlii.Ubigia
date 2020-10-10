namespace EtAlii.xTechnology.Hosting
{
    using System;

    public interface ICommand
    {
        string Name { get; }
        void Execute();
        bool CanExecute { get; }

        event EventHandler CanExecuteChanged;
    }
}
