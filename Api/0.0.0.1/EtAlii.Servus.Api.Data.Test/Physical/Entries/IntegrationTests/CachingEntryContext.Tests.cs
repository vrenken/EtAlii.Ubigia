namespace EtAlii.Servus.Api.Data.IntegrationTests
{
    using EtAlii.Servus.Api.Data.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CachingEntryContext_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void CachingEntryContext_Create()
        {
            // Arrange.
            var entryContext = new EntryContext(new EntryNotificationClientStub(), new EntryDataClientStub(), new ConnectionStatusProvider());
            var entryCacheProvider = new EntryCacheProvider();
            var entryCacheHelper = new EntryCacheHelper(entryCacheProvider);
            var entryCacheContextProvider = new EntryCacheContextProvider(entryContext);
            var entryCacheChangeHandler = new EntryCacheChangeHandler(entryCacheHelper, entryCacheContextProvider);
            var entryCacheGetHandler = new EntryCacheGetHandler(entryCacheHelper, entryCacheContextProvider);
            var entryCacheGetRelatedHandler = new EntryCacheGetRelatedHandler(entryCacheHelper, entryCacheGetHandler, entryCacheContextProvider);
            var entryCacheStoreHandler = new EntryCacheStoreHandler(entryCacheHelper, entryCacheProvider);

            // Act.
            var context = new CachingEntryContext(entryCacheContextProvider, entryCacheChangeHandler, entryCacheGetHandler, entryCacheGetRelatedHandler, entryCacheStoreHandler);

            // Assert.
            //Assert.IsNotNull(context);
        }
    }
}
