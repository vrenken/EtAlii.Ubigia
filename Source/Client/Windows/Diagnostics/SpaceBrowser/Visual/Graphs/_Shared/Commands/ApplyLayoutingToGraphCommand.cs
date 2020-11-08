namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.xTechnology.Structure.Workflow;


    public class ApplyLayoutingToGraphCommand : CommandBase
    {
        public IGraphDocumentViewModel ViewModel { get; }

        public ApplyLayoutingToGraphCommand(IGraphDocumentViewModel viewModel)
        {
            ViewModel = viewModel;
        }
    }
}
