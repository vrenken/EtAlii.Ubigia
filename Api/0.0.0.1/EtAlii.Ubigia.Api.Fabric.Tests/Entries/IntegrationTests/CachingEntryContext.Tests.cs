﻿namespace EtAlii.Ubigia.Api.Fabric.Tests
{
    using DataConnectionStub = EtAlii.Ubigia.Api.Transport.DataConnectionStub;
    using EtAlii.Ubigia.Api.Fabric;
    using Xunit;

    
    public class CachingEntryContextTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void CachingEntryContext_Create()
        {
            // Arrange.
            var entryContext = new EntryContext(new DataConnectionStub());
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
            //Assert.NotNull(context);
        }
    }
}
