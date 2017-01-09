namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using EtAlii.Servus.Client.Model;
    using EtAlii.Servus.Client.Windows.Shared;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Windows.Threading;
    using EtAlii.Servus.Api;


    public class DiscoverFromHead : IDiscoverFromHead
    {
        private readonly ISpaceConnection _connection;
        private readonly IUpdateEntryRelationsInGraph _updateEntryRelationsInGraph;
        private readonly IAddEntryToGraph _addEntryToGraph;

        public DiscoverFromHead(ISpaceConnection connection, IUpdateEntryRelationsInGraph updateEntryRelationsInGraph, IAddEntryToGraph addEntryToGraph)
        {
            _connection = connection;
            _updateEntryRelationsInGraph = updateEntryRelationsInGraph;
            _addEntryToGraph = addEntryToGraph;
        }

        public void Execute(EntryGraph graph)
        {
            var root = _connection.Roots.Get(DefaultRoot.Head);
            var entry = _connection.Entries.Get(root, EntryComponents.Relations);

            Discover(entry, graph);
        }

        private void Discover(Entry entry, EntryGraph graph)
        {
            _addEntryToGraph.Execute(entry.Id, graph);

            // TODO

            _updateEntryRelationsInGraph.Execute(entry.Id, graph);
        }
    }
}
