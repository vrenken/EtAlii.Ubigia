namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System.Collections.ObjectModel;

    public interface IDocumentsProvider
    {
        ObservableCollection<IDocumentViewModel> Documents { get; }
    }
}
