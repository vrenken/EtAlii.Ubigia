
namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.Workflow;
    using System.Linq;

    public class FindEntriesOnGraphQueryHandler : QueryHandlerBase<FindEntriesOnGraphQuery, IReadOnlyEntry>
    {
        private IGraphDocumentViewModel GraphViewModel { get { return _documentViewModelProvider.GetInstance<IGraphDocumentViewModel>(); } }
        private readonly DocumentViewModelProvider _documentViewModelProvider;


        public FindEntriesOnGraphQueryHandler(
            DocumentViewModelProvider documentViewModelProvider)  
        {
            _documentViewModelProvider = documentViewModelProvider;
        }

        protected override IQueryable<IReadOnlyEntry> Handle(FindEntriesOnGraphQuery query)
        {
            return query.Identifiers.Select(identifier => GraphViewModel.FindNodeByKey(identifier))
                .Select(entryNode => entryNode.Entry)
                .AsQueryable();
        }
    }
}
