namespace EtAlii.Servus.Api.Fabric
{
    using System;
    using System.Threading.Tasks;

    internal class ContentCacheRetrievePartHandler : IContentCacheRetrievePartHandler
    {
        private readonly IContentCacheHelper _cacheHelper;
        private readonly IContentCacheContextProvider _contextProvider;

        public ContentCacheRetrievePartHandler(
            IContentCacheHelper cacheHelper,
            IContentCacheContextProvider contextProvider)
        {
            _cacheHelper = cacheHelper;
            _contextProvider = contextProvider;
        }

        public async Task<IReadOnlyContentPart> Handle(Identifier identifier, UInt64 contentPartId)
        {
            var contentPart = _cacheHelper.Get(identifier, contentPartId);
            if (contentPart == null)
            {
                contentPart = await _contextProvider.Context.Retrieve(identifier, contentPartId);
                if (contentPart != null)
                {
                    _cacheHelper.Store(identifier, contentPart);
                }
            }
            return contentPart;
        }
    }
}
