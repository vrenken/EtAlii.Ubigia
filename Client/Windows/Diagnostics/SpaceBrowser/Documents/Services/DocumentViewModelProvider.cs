namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    public class DocumentViewModelProvider : IDocumentViewModelProvider
    {
        private IDocumentViewModel _viewModel;

        public void SetInstance(IDocumentViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public T GetInstance<T>()
            where T : IDocumentViewModel
        {
            return (T)_viewModel;
        }
    }
}
