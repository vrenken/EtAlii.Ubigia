namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.Workflow;


    public class AddEntryRelationsToGraphCommandHandler : CommandHandlerBase<AddEntryRelationsToGraphCommand>, IAddEntryRelationsToGraphCommandHandler
    {
        private readonly IDocumentViewModelProvider _documentViewModelProvider;

        private readonly IFabricContext _fabric;

        public AddEntryRelationsToGraphCommandHandler(
            IFabricContext fabric,
            IDocumentViewModelProvider documentViewModelProvider)
        {
            _fabric = fabric;
            _documentViewModelProvider = documentViewModelProvider;
        }

        protected override void Handle(AddEntryRelationsToGraphCommand command)
        {
        }
    }
}
