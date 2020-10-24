namespace EtAlii.Ubigia.Api.Fabric
{
    internal class ContentCacheContextProvider : IContentCacheContextProvider
    {
        public IContentContext Context { get; }

        public ContentCacheContextProvider(IContentContext context)
        {
            Context = context;
        }
    }
}
