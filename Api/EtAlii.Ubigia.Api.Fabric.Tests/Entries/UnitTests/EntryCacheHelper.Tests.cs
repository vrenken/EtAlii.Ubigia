﻿namespace EtAlii.Ubigia.Api.Fabric.Tests
{
    using EtAlii.Ubigia.Api.Fabric;
    using Xunit;

    public class EntryCacheHelperTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void EntryCacheHelper_Create()
        {
            // Arrange.
            var cacheProvider = new EntryCacheProvider();

            // Act.
            var helper = new EntryCacheHelper(cacheProvider);

            // Assert.
            Assert.NotNull(helper);
        }
    }
}