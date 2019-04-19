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
            //var configuration = _processConfigurationGetter.GetConfiguration(command.ProcessReason)
            //if (configuration.AutoRelate)
            //[
                //var vertex = command.Vertex
                //var entry = vertex.Entry
                //var entry = _fabric.Entries.Get(vertex.Entry.Id, EntryRelation.Relations | EntryRelation.Label)
                //UpdateParentRelation(_graph, vertex, entry)
                //UpdateChildRelations(_graph, vertex, entry)
            //]
        }

        //private void UpdateParentRelation(EntryGraph graph, EntryVertex vertex, Entry entry)
        //[
        //    if (entry.Parent != Relation.None)
        //    [
        //        var parentId = entry.Parent.Id
        //        var parentVertex = graph.Vertices.SingleOrDefault(v => v.Identifier == parentId)

        //        if (parentVertex != null)
        //        [
        //            if (!HasRelation(graph, vertex, parentVertex))
        //            [
        //                _dispatcher.SafeInvoke(delegate()
        //                [
        //                    graph.AddEdge(new RelationEdge(vertex, parentVertex))
        //                })
        //            ]
        //        ]
        //    ]
        //]
        //private void UpdateChildRelations(EntryGraph graph, EntryVertex vertex, Entry entry)
        //[
        //    if (entry.Children.Any())
        //    [
        //        foreach (var relation in entry.Children)
        //        [
        //            UpdateChildRelation(graph, vertex, relation.Id)
        //        ]
        //    ]
        //]
        //private void UpdateChildRelation(EntryGraph graph, EntryVertex vertex, Identifier childIdentifier)
        //[
        //    var childVertex = graph.Vertices.SingleOrDefault(v => v.Identifier == childIdentifier)

        //    if (childVertex != null)
        //    [
        //        if (!HasRelation(graph, childVertex, vertex))
        //        [
        //            _dispatcher.SafeInvoke(delegate()
        //            [
        //                graph.AddEdge(new RelationEdge(childVertex, vertex))
        //            })
        //        ]
        //    ]
        //]
        //private bool HasRelation(EntryGraph graph, EntryVertex source, EntryVertex target)
        //[
        //    return graph.ContainsEdge(source, target)
        //]
    }
}
