namespace EtAlii.Ubigia.Api.Fabric
{
    internal class PropertyCacheContextProvider : IPropertyCacheContextProvider
    {
        public IPropertiesContext Context { get; }

        public PropertyCacheContextProvider(IPropertiesContext context)
        {
            Context = context;
        }
    }
}
