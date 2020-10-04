namespace EtAlii.Ubigia.Api.Fabric
{
    internal class PropertiesCacheContextProvider : IPropertiesCacheContextProvider
    {
        public IPropertiesContext Context { get; }

        public PropertiesCacheContextProvider(IPropertiesContext context)
        {
            Context = context;
        }
    }
}
