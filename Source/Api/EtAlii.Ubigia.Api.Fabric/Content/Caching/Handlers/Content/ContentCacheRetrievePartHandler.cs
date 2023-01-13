// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric;

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

    public async Task<ContentPart> Handle(Identifier identifier, ulong contentPartId)
    {
        var contentPart = _cacheHelper.Get(identifier, contentPartId);
        if (contentPart == null)
        {
            contentPart = await _contextProvider.Context.Retrieve(identifier, contentPartId).ConfigureAwait(false);
            if (contentPart != null)
            {
                _cacheHelper.Store(identifier, contentPart);
            }
        }
        return contentPart;
    }
}
