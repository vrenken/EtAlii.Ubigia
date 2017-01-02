namespace EtAlii.Ubigia.Api.Logical
{
    public class HierarchicalContentManager : IHierarchicalContentManager
    {
        private readonly ILogicalContext _context;
        private readonly IContentManager _contentManager;

        public HierarchicalContentManager(ILogicalContext context, IContentManager contentManager)
        {
            _context = context;
            _contentManager = contentManager;
        }
    }
}
