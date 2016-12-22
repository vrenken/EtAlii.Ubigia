
namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Fabric;
    using EtAlii.xTechnology.Workflow;
    using System.Linq;
    using System.Windows.Threading;

    public class RemoveEntriesFromGraphCommandHandler : CommandHandlerBase<RemoveEntriesFromGraphCommand>
    {
        protected IGraphDocumentViewModel GraphViewModel { get { return _documentViewModelProvider.GetInstance<IGraphDocumentViewModel>(); } }
        private readonly DocumentViewModelProvider _documentViewModelProvider;
        private readonly MainDispatcherInvoker _mainDispatcherInvoker;

        private readonly object _lockObject = new object();

        public RemoveEntriesFromGraphCommandHandler(
            DocumentViewModelProvider documentViewModelProvider,
            GraphConfiguration configuration,
            MainDispatcherInvoker mainDispatcherInvoker)
        {
            _documentViewModelProvider = documentViewModelProvider;
            _mainDispatcherInvoker = mainDispatcherInvoker;
        }

        protected override void Handle(RemoveEntriesFromGraphCommand command)
        {
            _mainDispatcherInvoker.Invoke(delegate()
            {
                GraphViewModel.StartTransaction("NodeRemoved");

                foreach(var identifier in command.Identifiers)
                {
                    RemoveNode(identifier);
                }

                GraphViewModel.CommitTransaction("NodeRemoved");
            });
        }

        private void RemoveNode(Identifier identifier)
        {
            var entryNode = GraphViewModel.FindNodeByKey(identifier);
            if (entryNode != null)
            {
                lock (_lockObject)
                {
                    RemoveLinks(entryNode);
                    GraphViewModel.RemoveNode(entryNode);
                    GraphViewModel.DoNodeRemoved(entryNode);
                }
            }
        }

        private void RemoveLinks(EntryNode entryNode)
        {
            var entryLinks = GraphViewModel.GetLinksForNode(entryNode).ToArray();
            foreach (var entryLink in entryLinks)
            {
                GraphViewModel.RemoveLink(entryLink);
                GraphViewModel.DoLinkRemoved(entryLink);
            }
        }
    }
}
