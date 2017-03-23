namespace EtAlii.Ubigia.Api.Fabric
{
    internal class PropertyCacheContextProvider : IPropertyCacheContextProvider
    {
        public IPropertyContext Context { get; }

        public PropertyCacheContextProvider(IPropertyContext context)
        {
            Context = context;
        }
    }
}
