namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.Structure.Workflow;

    public class DiscoverEntryCommandHandler : CommandHandlerBase<DiscoverEntryCommand>, IDiscoverEntryCommandHandler
    {
        private readonly IFabricContext _fabric;
        protected IGraphDocumentViewModel GraphViewModel => _documentViewModelProvider.GetInstance<IGraphDocumentViewModel>();
        private readonly IDocumentViewModelProvider _documentViewModelProvider;
        private readonly IGraphConfiguration _configuration;
        private readonly IGraphContext _graphContext;

        public DiscoverEntryCommandHandler(
            IFabricContext fabric,
            IDocumentViewModelProvider documentViewModelProvider,
            IGraphConfiguration configuration, 
            IGraphContext graphContext)
        {
            _configuration = configuration;
            _graphContext = graphContext;
            _fabric = fabric;
            _documentViewModelProvider = documentViewModelProvider;
        }

        protected override void Handle(DiscoverEntryCommand command)
        {
            if (command.Depth > 0)
            {
                var processReason = command.ProcessReason;

                var graphDocumentViewModel = _documentViewModelProvider.GetInstance<IGraphDocumentViewModel>();

                var addEntryToGraphCommand = new AddEntryToGraphCommand(command.Entry, processReason, graphDocumentViewModel);
                _graphContext.CommandProcessor.Process(addEntryToGraphCommand, _graphContext.AddEntryToGraphCommandHandler);

                var entry = command.Entry;
                var depth = command.Depth - 1;

                if (_configuration.ShowHierarchical)
                {
                    EnqueueForDiscovery(entry.Parent, depth);//, processReason
                    EnqueueForDiscovery(entry.Children, depth);//, processReason
                }
                if (_configuration.ShowSequential)
                {
                    EnqueueForDiscovery(entry.Next, depth);//, processReason
                    EnqueueForDiscovery(entry.Previous, depth);//, processReason
                }
                if (_configuration.ShowTemporal)
                {
                    EnqueueForDiscovery(entry.Downdate, depth);//, processReason
                    EnqueueForDiscovery(entry.Updates, depth);//, processReason
                }
            }
        }

        private void EnqueueForDiscovery(Relation relation, int depth)//, ProcessReason processReason
        {
            if (relation != Relation.None)
            {
                EnqueueForDiscovery(relation.Id, depth);//, processReason
            }
        }

        private void EnqueueForDiscovery(IEnumerable<Relation> relations, int depth)//, ProcessReason processReason
        {
            foreach(var relation in relations)
            {
                EnqueueForDiscovery(relation.Id, depth);//, processReason
            }
        }

        private void EnqueueForDiscovery(Identifier identifier, int depth)//, ProcessReason processReason
        {
            if (GraphViewModel.FindNodeByKey(identifier) == null)
            {
                IReadOnlyEntry entry = null;
                var task = Task.Run(async () =>
                {
                    entry = await _fabric.Entries.Get(identifier, new ExecutionScope(false));
                });
                task.Wait();

                _graphContext.CommandProcessor.Process(new DiscoverEntryCommand(entry, ProcessReason.Discovered, depth), _graphContext.DiscoverEntryCommandHandler);
            }
        }
    }
}
