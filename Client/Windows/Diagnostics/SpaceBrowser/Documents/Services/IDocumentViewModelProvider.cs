namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    public interface IDocumentViewModelProvider
    {
        void SetInstance(IDocumentViewModel viewModel);

        T GetInstance<T>()
            where T : IDocumentViewModel;
    }
}