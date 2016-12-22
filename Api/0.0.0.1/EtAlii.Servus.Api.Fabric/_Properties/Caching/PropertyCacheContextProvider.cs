namespace EtAlii.Servus.Api.Fabric
{
    internal class PropertyCacheContextProvider : IPropertyCacheContextProvider
    {
        public IPropertyContext Context { get { return _context; } }
        private readonly IPropertyContext _context;

        public PropertyCacheContextProvider(IPropertyContext context)
        {
            _context = context;
        }
    }
}
