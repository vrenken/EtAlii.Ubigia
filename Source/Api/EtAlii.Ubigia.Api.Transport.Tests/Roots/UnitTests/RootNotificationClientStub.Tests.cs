// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Tests
{
    using EtAlii.Ubigia.Tests;
    using Xunit;

    [CorrelateUnitTests]
    public class RootNotificationClientStubTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootNotificationClientStub_Create()
        {
            // Arrange.

            // Act.
            var rootNotificationClientStub = new RootNotificationClientStub();

            // Assert.
            Assert.NotNull(rootNotificationClientStub);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootNotificationClientStub_Connect()
        {
            // Arrange.
            var rootNotificationClientStub = new RootNotificationClientStub();

            // Act.
            rootNotificationClientStub.Connect(null);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootNotificationClientStub_Disconnect()
        {
            // Arrange.
            var rootNotificationClientStub = new RootNotificationClientStub();

            // Act.
            rootNotificationClientStub.Disconnect();

            // Assert.
        }
    }
}
