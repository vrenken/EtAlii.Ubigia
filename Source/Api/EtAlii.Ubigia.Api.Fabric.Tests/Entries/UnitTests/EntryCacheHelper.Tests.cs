﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Tests
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
