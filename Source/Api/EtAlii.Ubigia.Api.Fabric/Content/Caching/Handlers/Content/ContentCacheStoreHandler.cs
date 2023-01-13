// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric;

using System.Threading.Tasks;

internal class ContentCacheStoreHandler : IContentCacheStoreHandler
{
    private readonly IContentCacheHelper _cacheHelper;
    private readonly IContentCacheContextProvider _contextProvider;

    public ContentCacheStoreHandler(
        IContentCacheHelper cacheHelper,
        IContentCacheContextProvider contextProvider)
    {
        _cacheHelper = cacheHelper;
        _contextProvider = contextProvider;
    }


    public async Task Handle(Identifier identifier, Content content)
    {
        await _contextProvider.Context.Store(identifier, content).ConfigureAwait(false);

        if (content.Summary != null && content.Summary.IsComplete)
        {
            _cacheHelper.Store(identifier, content);
        }
    }
}
