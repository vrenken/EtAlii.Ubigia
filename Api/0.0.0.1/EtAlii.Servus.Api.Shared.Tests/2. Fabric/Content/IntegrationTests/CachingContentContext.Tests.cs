namespace EtAlii.Servus.Api.Fabric.Tests
{
    using EtAlii.Servus.Api.Tests;
    using DataConnectionStub = EtAlii.Servus.Api.Transport.DataConnectionStub;
    using EtAlii.Servus.Api.Fabric;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CachingContentContext_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
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
            //Assert.IsNotNull(context);
        }
    }
}
