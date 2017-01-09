﻿namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Input;
    using EtAlii.Ubigia.Api;

    public interface IRootsViewModel : INotifyPropertyChanged
    {
        IEnumerable<Root> AvailableRoots { get; set; }
        Root SelectedRoot { get; set; }
        ICommand BeginEntryDragCommand { get; }
    }
}