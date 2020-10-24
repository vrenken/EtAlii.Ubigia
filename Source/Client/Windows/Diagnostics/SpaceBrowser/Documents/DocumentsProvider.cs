namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.Collections.ObjectModel;

    public class DocumentsProvider : IDocumentsProvider
    {
        public ObservableCollection<IDocumentViewModel> Documents { get; } = new ObservableCollection<IDocumentViewModel>();
    }
}