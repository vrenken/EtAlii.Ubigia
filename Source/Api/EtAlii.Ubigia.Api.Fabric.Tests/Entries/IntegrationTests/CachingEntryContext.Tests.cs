// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Tests
{
    using DataConnectionStub = EtAlii.Ubigia.Api.Transport.DataConnectionStub;
    using EtAlii.Ubigia.Api.Fabric;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public class CachingEntryContextTests
    {
        [Fact]
        public void CachingEntryContext_Create()
        {
            // Arrange.
            var entryContext = new EntryContext(new DataConnectionStub());
            var entryCacheHelper = new EntryCacheHelper();
            var entryCacheContextProvider = new EntryCacheContextProvider(entryContext);
            var entryCacheChangeHandler = new EntryCacheChangeHandler(entryCacheHelper, entryCacheContextProvider);
            var entryCacheGetHandler = new EntryCacheGetHandler(entryCacheHelper, entryCacheContextProvider);
            var entryCacheGetRelatedHandler = new EntryCacheGetRelatedHandler(entryCacheHelper, entryCacheGetHandler, entryCacheContextProvider);

            // Act.
            var context = new CachingEntryContext(entryCacheContextProvider, entryCacheChangeHandler, entryCacheGetHandler, entryCacheGetRelatedHandler);

            // Assert.
            Assert.NotNull(context);
        }
    }
}
