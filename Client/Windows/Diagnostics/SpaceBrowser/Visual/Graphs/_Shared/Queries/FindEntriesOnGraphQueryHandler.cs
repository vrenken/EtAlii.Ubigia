
namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.Ubigia.Api;
    using EtAlii.xTechnology.Workflow;
    using System.Linq;

    public class FindEntriesOnGraphQueryHandler : QueryHandlerBase<FindEntriesOnGraphQuery, IReadOnlyEntry>, IFindEntriesOnGraphQueryHandler
    {
        private IGraphDocumentViewModel GraphViewModel => _documentViewModelProvider.GetInstance<IGraphDocumentViewModel>();
        private readonly IDocumentViewModelProvider _documentViewModelProvider;


        public FindEntriesOnGraphQueryHandler(
            IDocumentViewModelProvider documentViewModelProvider)  
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
