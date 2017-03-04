namespace EtAlii.Ubigia.Api.Fabric
{
    internal class PropertyCacheContextProvider : IPropertyCacheContextProvider
    {
        public IPropertyContext Context => _context;
        private readonly IPropertyContext _context;

        public PropertyCacheContextProvider(IPropertyContext context)
        {
            _context = context;
        }
    }
}
