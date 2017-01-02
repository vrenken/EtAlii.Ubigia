namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.Workflow;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AddEntryToGraphCommandHandler : CommandHandlerBase<AddEntryToGraphCommand>
    {
        private readonly IFabricContext _fabric;
        private readonly GraphConfiguration _configuration;
        private readonly MainDispatcherInvoker _mainDispatcherInvoker;
        protected IGraphDocumentViewModel GraphViewModel { get { return _documentViewModelProvider.GetInstance<IGraphDocumentViewModel>(); } }
        private readonly DocumentViewModelProvider _documentViewModelProvider;

        private readonly object _lockObject = new object();

        public AddEntryToGraphCommandHandler(
            IFabricContext fabric, 
            DocumentViewModelProvider documentViewModelProvider,
            GraphConfiguration configuration,
            MainDispatcherInvoker mainDispatcherInvoker)
        {
            _fabric = fabric;
            _documentViewModelProvider = documentViewModelProvider;
            _configuration = configuration;
            _mainDispatcherInvoker = mainDispatcherInvoker;
        }

        protected override void Handle(AddEntryToGraphCommand command)
        {
            _mainDispatcherInvoker.SafeInvoke(delegate()
            {
                var entry = command.Entry;
                var identifier = entry.Id;

                AddNode(identifier);

                if (_configuration.ShowHierarchical)
                {
                    AddFromRelation(identifier, entry.Parent, EntryRelation.Parent);
                    AddFromRelations(identifier, entry.Children, EntryRelation.Child);
                }
                if (_configuration.ShowSequential)
                {
                    AddFromRelation(identifier, entry.Previous, EntryRelation.Previous);
                    AddFromRelation(identifier, entry.Next, EntryRelation.Next);
                }
                if (_configuration.ShowTemporal)
                {
                    AddFromRelation(identifier, entry.Downdate, EntryRelation.Downdate);
                    AddFromRelations(identifier, entry.Updates, EntryRelation.Update);
                }
            });
        }

        private void AddFromRelation(Identifier entryIdentifier, Relation relation, EntryRelation entryRelation)
        {
            if (relation != Relation.None)
            {
                AddNode(relation.Id);
                AddRelation(entryIdentifier, relation.Id, entryRelation);
            }
        }

        private void AddFromRelations(Identifier entryIdentifier, IEnumerable<Relation> relations, EntryRelation entryRelation)
        {
            foreach (var relation in relations)
            {
                AddNode(relation.Id);
                AddRelation(entryIdentifier, relation.Id, entryRelation);
            }
        }

        private void AddNode(Identifier identifier)
        {
            if (GraphViewModel.FindNodeByKey(identifier) == null)
            {
                lock (_lockObject)
                {
                    GraphViewModel.StartTransaction("NodeAdd");

                    IReadOnlyEntry result = null;
                    var task = Task.Run(async () =>
                    {
                        result = await _fabric.Entries.Get(identifier, new ExecutionScope(false));
                    });
                    task.Wait();

                    var entry = result;
                    var entryNode = new EntryNode(entry);
                    GraphViewModel.AddNode(entryNode);
                    GraphViewModel.DoNodeAdded(entryNode);
                    GraphViewModel.CommitTransaction("NodeAdd");
                }
            }
        }

        private void AddRelation(Identifier from, Identifier to, EntryRelation entryRelation)
        {
            var fromNode = GraphViewModel.FindNodeByKey(from);
            var toNode = GraphViewModel.FindNodeByKey(to);

            var hasLink = GraphViewModel.IsLinked(fromNode, null, toNode, null);
            hasLink |= GraphViewModel.IsLinked(toNode, null, fromNode, null);
            if (!hasLink)
            {
                lock (_lockObject)
                {
                    GraphViewModel.StartTransaction("LinkAdd");
                    var entryLink = new EntryLink { From = fromNode.Key, To = toNode.Key, Category = entryRelation.ToString() };
                    GraphViewModel.AddLink(entryLink);
                    GraphViewModel.DoLinkAdded(entryLink);
                    GraphViewModel.CommitTransaction("LinkAdd");
                }
            }
        }
    }
}
