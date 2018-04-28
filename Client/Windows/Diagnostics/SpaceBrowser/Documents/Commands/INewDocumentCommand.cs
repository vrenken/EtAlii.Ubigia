namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.Windows.Input;

    public interface INewDocumentCommand : ICommand
    {
        string Icon { get; }
        string Header { get; }
        string InfoLine { get; }
        string InfoTip1 { get; }
        string InfoTip2 { get; }
        string TitleFormat { get; }
        IDocumentFactory DocumentFactory { get; }

        void Initialize(IMainWindowViewModel mainWindowViewModel);
    }
}