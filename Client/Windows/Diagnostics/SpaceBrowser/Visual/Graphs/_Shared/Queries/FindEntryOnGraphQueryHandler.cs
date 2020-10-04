
namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.xTechnology.Workflow;

    public class FindEntryOnGraphQueryHandler : QueryHandlerBase<FindEntryOnGraphQuery, IReadOnlyEntry>, IFindEntryOnGraphQueryHandler
    {
        private IGraphDocumentViewModel GraphViewModel => _documentViewModelProvider.GetInstance<IGraphDocumentViewModel>();
        private readonly IDocumentViewModelProvider _documentViewModelProvider;


        public FindEntryOnGraphQueryHandler(
            IDocumentViewModelProvider documentViewModelProvider)  
        {
            _documentViewModelProvider = documentViewModelProvider;
        }

        protected override IQueryable<IReadOnlyEntry> Handle(FindEntryOnGraphQuery query)
        {
            var result = new List<IReadOnlyEntry>();

            var entryNode = GraphViewModel.FindNodeByKey(query.Identifier);
            if(entryNode != null)
            {
                result.Add(entryNode.Entry);
            }
            return result.AsQueryable();
        }
    }
}
