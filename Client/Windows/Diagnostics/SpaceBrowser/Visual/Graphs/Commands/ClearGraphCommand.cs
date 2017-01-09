
namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using EtAlii.Servus.Client.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;

    public class ClearGraphCommand : ICommand
    {
        public ClearGraphCommand()
        {
        }

        public bool CanExecute(object parameter)
        {
            var graphViewModel = parameter as GraphDocumentViewModel;
            var canExecute = false;
            if (graphViewModel != null)
            {
                canExecute = true;// graphViewModel.NodesSource.Count() != 0;
            }
            return canExecute;
        }

        public event EventHandler CanExecuteChanged = delegate { };

        public void Execute(object parameter)
        {
            var graphViewModel = parameter as GraphDocumentViewModel;
            graphViewModel.Clear();
        }
    }
}
