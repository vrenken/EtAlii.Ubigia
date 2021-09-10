// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using System;
    using EtAlii.Ubigia.Api.Transport.Management.Grpc;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public class GuidTest
    {
        [Fact]
        public void Guid_SystemGuidToWireGuid()
        {
            // Arrange.
            var guid = Guid.NewGuid();

            // Act.
            var wireGuid = guid.ToWire();

            // Assert
            Assert.NotEqual<ulong>(0, wireGuid.Data1);
            Assert.NotEqual<ulong>(0, wireGuid.Data2);
        }

        [Fact]
        public void Guid_SystemGuidToWireGuid_And_Back()
        {
            // Arrange.
            var guid = Guid.NewGuid();
            var wireGuid = guid.ToWire();

            // Act.
            var secondGuid = wireGuid.ToLocal();

            // Assert
            Assert.Equal(guid, secondGuid);
        }
    }
}
