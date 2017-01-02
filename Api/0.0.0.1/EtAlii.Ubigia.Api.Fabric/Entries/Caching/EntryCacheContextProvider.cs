namespace EtAlii.Ubigia.Api.Fabric
{
    internal class EntryCacheContextProvider : IEntryCacheContextProvider
    {
        public IEntryContext Context { get { return _context; } }
        private readonly IEntryContext _context;

        public EntryCacheContextProvider(IEntryContext context)
        {
            _context = context;
        }
    }
}
