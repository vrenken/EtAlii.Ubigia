// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Tests
{
    using EtAlii.Ubigia.Api.Fabric;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public class EntryCacheHelperTests
    {
        [Fact]
        public void EntryCacheHelper_Create()
        {
            // Arrange.

            // Act.
            var helper = new EntryCacheHelper();

            // Assert.
            Assert.NotNull(helper);
        }
    }
}
