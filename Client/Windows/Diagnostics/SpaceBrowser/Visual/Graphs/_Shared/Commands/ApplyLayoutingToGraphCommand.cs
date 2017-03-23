namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.xTechnology.Workflow;


    public class ApplyLayoutingToGraphCommand : CommandBase
    {
        public IGraphDocumentViewModel ViewModel { get; }

        public ApplyLayoutingToGraphCommand(IGraphDocumentViewModel viewModel)
        {
            ViewModel = viewModel;
        }
    }
}
