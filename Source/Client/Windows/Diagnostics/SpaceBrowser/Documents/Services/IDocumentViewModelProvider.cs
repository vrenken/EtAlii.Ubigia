namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    public interface IDocumentViewModelProvider
    {
        void SetInstance(IDocumentViewModel viewModel);

        T GetInstance<T>()
            where T : IDocumentViewModel;
    }
}