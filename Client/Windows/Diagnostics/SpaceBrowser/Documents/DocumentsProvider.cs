namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System.Collections.ObjectModel;

    public class DocumentsProvider : IDocumentsProvider
    {
        public ObservableCollection<IDocumentViewModel> Documents { get; } = new ObservableCollection<IDocumentViewModel>();
    }
}