
namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api;
    using EtAlii.xTechnology.Workflow;
    using System.Linq;

    public class RemoveEntriesFromGraphCommandHandler : CommandHandlerBase<RemoveEntriesFromGraphCommand>, IRemoveEntriesFromGraphCommandHandler
    {
        protected IGraphDocumentViewModel GraphViewModel => _documentViewModelProvider.GetInstance<IGraphDocumentViewModel>();
        private readonly IDocumentViewModelProvider _documentViewModelProvider;
        private readonly IMainDispatcherInvoker _mainDispatcherInvoker;

        private readonly object _lockObject = new object();

        public RemoveEntriesFromGraphCommandHandler(
            IDocumentViewModelProvider documentViewModelProvider,
            IGraphConfiguration configuration,
            IMainDispatcherInvoker mainDispatcherInvoker)
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
