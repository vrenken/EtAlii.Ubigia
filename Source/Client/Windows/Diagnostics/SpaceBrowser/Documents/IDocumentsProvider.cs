namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.Collections.ObjectModel;

    public interface IDocumentsProvider
    {
        ObservableCollection<IDocumentViewModel> Documents { get; }
    }
}
