
namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.Ubigia.Api;
    using EtAlii.xTechnology.Workflow;

    public class FindEntryOnGraphQueryHandler : QueryHandlerBase<FindEntryOnGraphQuery, IReadOnlyEntry>
    {
        private IGraphDocumentViewModel GraphViewModel { get { return _documentViewModelProvider.GetInstance<IGraphDocumentViewModel>(); } }
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
