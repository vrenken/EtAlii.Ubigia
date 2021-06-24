// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Local
{
    using Xunit;

    public class StartupLoaderTests
    {
        [Fact]
        public void StartupLoader_Create()
        {
            // Arrange.

            // Act.
            var loader = new StartupLoader();

            // Assert.
            Assert.NotNull(loader);
        }
    }
}