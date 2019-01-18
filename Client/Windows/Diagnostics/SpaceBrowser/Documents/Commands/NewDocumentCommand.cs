namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Linq;

    public class NewDocumentCommandBase : INewDocumentCommand
    {
        private readonly IDocumentContext _documentContext;

        private IMainWindowViewModel _mainWindowViewModel;

        public string Icon { get; set; } = "";

        public string Header { get; set; } = "";

        public string InfoLine { get; set; } = "";

        public string InfoTip1 { get; set; } = "";

        public string InfoTip2 { get; set; } = "";

        public string TitleFormat { get; set; } = "No name {0}";

        public IDocumentFactory DocumentFactory { get; protected set; }

        public NewDocumentCommandBase(IDocumentContext documentContext)
        {
            _documentContext = documentContext;
        }

        public void Initialize(IMainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged = delegate { };

        public void Execute(object parameter)
        {
            var title = DetermineTitle();

            var documentViewModel = DocumentFactory.Create(_documentContext);
            documentViewModel.Title = title;
            _mainWindowViewModel.Documents.Add(documentViewModel);
        }

        private string DetermineTitle()
        {
            var i = 0;
            var title = String.Empty;
            do
            {
                title = String.Format(TitleFormat, ++i);
            }
            while (_mainWindowViewModel.Documents.Any(g => g.Title == title));
            return title;
        }
    }
}
