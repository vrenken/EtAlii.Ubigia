namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.xTechnology.Workflow;


    public class ApplyLayoutingToGraphCommand : CommandBase
    {
        public IGraphDocumentViewModel ViewModel { get { return _viewModel; } }
        private readonly IGraphDocumentViewModel _viewModel;

        public ApplyLayoutingToGraphCommand(IGraphDocumentViewModel viewModel)
        {
            _viewModel = viewModel;
        }
    }
}
