namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.xTechnology.Workflow;


    public class ApplyLayoutingToGraphCommand : CommandBase<ApplyLayoutingToGraphCommandHandler>
    {
        public GraphDocumentViewModel ViewModel { get { return _viewModel; } }
        private readonly GraphDocumentViewModel _viewModel;

        public ApplyLayoutingToGraphCommand(GraphDocumentViewModel viewModel)
        {
            _viewModel = viewModel;
        }
    }
}
