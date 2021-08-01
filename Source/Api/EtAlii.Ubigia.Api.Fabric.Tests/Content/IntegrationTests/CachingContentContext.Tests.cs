// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Tests
{
    using DataConnectionStub = EtAlii.Ubigia.Api.Transport.DataConnectionStub;
    using EtAlii.Ubigia.Api.Fabric;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public class CachingContentContextTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void CachingContentContext_Create()
        {
            // Arrange.
            var contentContext = new ContentContext(new DataConnectionStub());
            var contentCacheProvider = new ContentCacheProvider();
            var contentCacheHelper = new ContentCacheHelper(contentCacheProvider);
            var contentDefinitionCacheHelper = new ContentDefinitionCacheHelper(contentCacheProvider);
            var contentCacheContextProvider = new ContentCacheContextProvider(contentContext);
            var contentCacheRetrieveDefinitionHandler = new ContentCacheRetrieveDefinitionHandler(contentDefinitionCacheHelper, contentCacheContextProvider);
            var contentCacheStoreDefinitionHandler = new ContentCacheStoreDefinitionHandler(contentDefinitionCacheHelper, contentCacheContextProvider);
            var contentCacheRetrieveHandler = new ContentCacheRetrieveHandler(contentCacheHelper, contentCacheContextProvider);
            var contentCacheRetrievePartHandler = new ContentCacheRetrievePartHandler(contentCacheHelper, contentCacheContextProvider);
            var contentCacheStoreHandler = new ContentCacheStoreHandler(contentCacheHelper, contentCacheContextProvider);
            var contentCacheStorePartHandler = new ContentCacheStorePartHandler(contentCacheHelper, contentCacheContextProvider);

            // Act.
            var context = new CachingContentContext(contentCacheContextProvider, contentCacheRetrieveDefinitionHandler, contentCacheStoreDefinitionHandler, contentCacheRetrieveHandler, contentCacheRetrievePartHandler, contentCacheStoreHandler, contentCacheStorePartHandler);

            // Assert.
            Assert.NotNull(context);
        }
    }
}
