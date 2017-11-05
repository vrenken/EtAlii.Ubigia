namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Diagnostics;
    using System.Windows.Input;

    /// <summary>
    /// From http://msdn.microsoft.com/en-us/magazine/dd419663.aspx
    /// </summary>
    public class WrappedHostCommand : ICommand
    {
        private readonly IHostCommand _hostCommand;

        public WrappedHostCommand(IHostCommand hostCommand)
        {
            _hostCommand = hostCommand;
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _hostCommand.CanExecute;
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should 
        /// execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => _hostCommand.CanExecuteChanged += value;
            remove => _hostCommand.CanExecuteChanged -= value;
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does 
        /// not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            _hostCommand.Execute();
        }
    }
}
