namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.Workflow;

    public class AddEntryToGraphCommandHandler : CommandHandlerBase<AddEntryToGraphCommand>, IAddEntryToGraphCommandHandler
    {
        private readonly IFabricContext _fabric;
        private readonly IGraphConfiguration _configuration;
        private readonly IMainDispatcherInvoker _mainDispatcherInvoker;
        //protected IGraphDocumentViewModel GraphViewModel [ get [ return _documentViewModelProvider.GetInstance<IGraphDocumentViewModel>(); ] ]
        //private readonly IDocumentViewModelProvider _documentViewModelProvider

        private readonly object _lockObject = new object();

        public AddEntryToGraphCommandHandler(
            IFabricContext fabric,
            //IDocumentViewModelProvider documentViewModelProvider,
            IGraphConfiguration configuration,
            IMainDispatcherInvoker mainDispatcherInvoker)
        {
            _fabric = fabric;
            //_documentViewModelProvider = documentViewModelProvider
            _configuration = configuration;
            _mainDispatcherInvoker = mainDispatcherInvoker;
        }

        protected override void Handle(AddEntryToGraphCommand command)
        {
            _mainDispatcherInvoker.SafeInvoke(delegate
            {
                var entry = command.Entry;
                var identifier = entry.Id;

                AddNode(identifier, command);

                if (_configuration.ShowHierarchical)
                {
                    AddFromRelation(identifier, entry.Parent, EntryRelation.Parent, command);
                    AddFromRelations(identifier, entry.Children, EntryRelation.Child, command);
                }
                if (_configuration.ShowSequential)
                {
                    AddFromRelation(identifier, entry.Previous, EntryRelation.Previous, command);
                    AddFromRelation(identifier, entry.Next, EntryRelation.Next, command);
                }
                if (_configuration.ShowTemporal)
                {
                    AddFromRelation(identifier, entry.Downdate, EntryRelation.Downdate, command);
                    AddFromRelations(identifier, entry.Updates, EntryRelation.Update, command);
                }
            });
        }

        private void AddFromRelation(Identifier entryIdentifier, Relation relation, EntryRelation entryRelation, AddEntryToGraphCommand command)
        {
            if (relation != Relation.None)
            {
                AddNode(relation.Id, command);
                AddRelation(entryIdentifier, relation.Id, entryRelation, command);
            }
        }

        private void AddFromRelations(Identifier entryIdentifier, IEnumerable<Relation> relations, EntryRelation entryRelation, AddEntryToGraphCommand command)
        {
            foreach (var relation in relations)
            {
                AddNode(relation.Id, command);
                AddRelation(entryIdentifier, relation.Id, entryRelation, command);
            }
        }

        private void AddNode(Identifier identifier, AddEntryToGraphCommand command)
        {
            if (command.GraphDocumentViewModel.FindNodeByKey(identifier) == null)
            {
                lock (_lockObject)
                {
                    command.GraphDocumentViewModel.StartTransaction("NodeAdd");

                    IReadOnlyEntry result = null;
                    var task = Task.Run(async () =>
                    {
                        result = await _fabric.Entries.Get(identifier, new ExecutionScope(false));
                    });
                    task.Wait();

                    var entry = result;
                    var entryNode = new EntryNode(entry);
                    command.GraphDocumentViewModel.AddNode(entryNode);
                    command.GraphDocumentViewModel.DoNodeAdded(entryNode);
                    command.GraphDocumentViewModel.CommitTransaction("NodeAdd");
                }
            }
        }

        private void AddRelation(Identifier from, Identifier to, EntryRelation entryRelation, AddEntryToGraphCommand command)
        {
            var fromNode = command.GraphDocumentViewModel.FindNodeByKey(from);
            var toNode = command.GraphDocumentViewModel.FindNodeByKey(to);

            var hasLink = command.GraphDocumentViewModel.IsLinked(fromNode, null, toNode, null);
            hasLink |= command.GraphDocumentViewModel.IsLinked(toNode, null, fromNode, null);
            if (!hasLink)
            {
                lock (_lockObject)
                {
                    command.GraphDocumentViewModel.StartTransaction("LinkAdd");
                    var entryLink = new EntryLink { From = fromNode.Key, To = toNode.Key, Category = entryRelation.ToString() };
                    command.GraphDocumentViewModel.AddLink(entryLink);
                    command.GraphDocumentViewModel.DoLinkAdded(entryLink);
                    command.GraphDocumentViewModel.CommitTransaction("LinkAdd");
                }
            }
        }
    }
}
