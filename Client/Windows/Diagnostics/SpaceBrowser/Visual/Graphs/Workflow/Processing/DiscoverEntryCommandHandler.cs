namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.Workflow;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class DiscoverEntryCommandHandler : CommandHandlerBase<DiscoverEntryCommand>
    {
        private readonly IFabricContext _fabric;
        protected IGraphDocumentViewModel GraphViewModel { get { return _documentViewModelProvider.GetInstance<IGraphDocumentViewModel>(); } }
        private readonly IDocumentViewModelProvider _documentViewModelProvider;
        private readonly ICommandProcessor _commandProcessor;
        private readonly GraphConfiguration _configuration;

        public DiscoverEntryCommandHandler(
            IFabricContext fabric,
            ICommandProcessor commandProcessor,
            IDocumentViewModelProvider documentViewModelProvider,
            GraphConfiguration configuration)
        {
            _configuration = configuration;
            _fabric = fabric;
            _commandProcessor = commandProcessor;
            _documentViewModelProvider = documentViewModelProvider;
        }

        protected override void Handle(DiscoverEntryCommand command)
        {
            if (command.Depth > 0)
            {
                var processReason = command.ProcessReason;
                _commandProcessor.Process(new AddEntryToGraphCommand(command.Entry, processReason));

                var entry = command.Entry;
                var depth = command.Depth - 1;

                if (_configuration.ShowHierarchical)
                {
                    EnqueueForDiscovery(entry.Parent, depth, processReason);
                    EnqueueForDiscovery(entry.Children, depth, processReason);
                }
                if (_configuration.ShowSequential)
                {
                    EnqueueForDiscovery(entry.Next, depth, processReason);
                    EnqueueForDiscovery(entry.Previous, depth, processReason);
                }
                if (_configuration.ShowTemporal)
                {
                    EnqueueForDiscovery(entry.Downdate, depth, processReason);
                    EnqueueForDiscovery(entry.Updates, depth, processReason);
                }
            }
        }

        private void EnqueueForDiscovery(Relation relation, int depth, ProcessReason processReason)
        {
            if (relation != Relation.None)
            {
                EnqueueForDiscovery(relation.Id, depth, processReason);
            }
        }

        private void EnqueueForDiscovery(IEnumerable<Relation> relations, int depth, ProcessReason processReason)
        {
            foreach(var relation in relations)
            {
                EnqueueForDiscovery(relation.Id, depth, processReason);
            }
        }

        private void EnqueueForDiscovery(Identifier identifier, int depth, ProcessReason processReason)
        {
            if (GraphViewModel.FindNodeByKey(identifier) == null)
            {
                IReadOnlyEntry entry = null;
                var task = Task.Run(async () =>
                {
                    entry = await _fabric.Entries.Get(identifier, new ExecutionScope(false));
                });
                task.Wait();

                _commandProcessor.Process(new DiscoverEntryCommand(entry, ProcessReason.Discovered, depth));
            }
        }
    }
}
