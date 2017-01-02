namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;

    public interface IProfilingViewModel : IDocumentViewModel
    {
        //ScriptButtonsViewModel Buttons { get; }
        IProfilingAspectsViewModel Aspects { get; }

        ReadOnlyObservableCollection<ProfilingResult> Results { get; }

        /// <summary>
        /// Multicast event for property change notifications.
        /// </summary>
        event PropertyChangedEventHandler PropertyChanged;
    }
}