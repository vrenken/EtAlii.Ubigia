// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Tests
{
    using Xunit;

    public class ContentNotificationClientStubTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentNotificationClientStub_Create()
        {
            // Arrange.

            // Act,
            var contentNotificationClientStub = new ContentNotificationClientStub();

            // Assert.
            Assert.NotNull(contentNotificationClientStub);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentNotificationClientStub_Connect()
        {
            // Arrange.
            var contentNotificationClientStub = new ContentNotificationClientStub();

            // Act,
            contentNotificationClientStub.Connect(null);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentNotificationClientStub_Disconnect()
        {
            // Arrange.
            var contentNotificationClientStub = new ContentNotificationClientStub();

            // Act,
            contentNotificationClientStub.Disconnect();

            // Assert.
        }
    }
}
