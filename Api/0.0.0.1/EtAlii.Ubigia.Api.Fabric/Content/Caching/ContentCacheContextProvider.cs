namespace EtAlii.Ubigia.Api.Fabric
{
    internal class ContentCacheContextProvider : IContentCacheContextProvider
    {
        public IContentContext Context => _context;
        private readonly IContentContext _context;

        public ContentCacheContextProvider(IContentContext context)
        {
            _context = context;
        }
    }
}
