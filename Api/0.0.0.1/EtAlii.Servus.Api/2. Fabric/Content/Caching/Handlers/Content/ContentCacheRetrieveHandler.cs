namespace EtAlii.Servus.Api.Fabric
{
    using System.Threading.Tasks;

    internal class ContentCacheRetrieveHandler : IContentCacheRetrieveHandler
    {
        private readonly IContentCacheHelper _cacheHelper;
        private readonly IContentCacheContextProvider _contextProvider;

        public ContentCacheRetrieveHandler(
            IContentCacheHelper cacheHelper,
            IContentCacheContextProvider contextProvider)
        {
            _cacheHelper = cacheHelper;
            _contextProvider = contextProvider;
        }

        public async Task<IReadOnlyContent> Handle(Identifier identifier)
        {
            var content = _cacheHelper.Get(identifier);
            if (content == null)
            {
                content = await _contextProvider.Context.Retrieve(identifier);
                if (content != null && content.Summary.IsComplete)
                {
                    _cacheHelper.Store(identifier, content);
                }
            }
            return content;
        }
    }
}
