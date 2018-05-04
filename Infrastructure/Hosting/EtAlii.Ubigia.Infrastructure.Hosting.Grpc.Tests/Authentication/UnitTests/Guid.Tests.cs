namespace EtAlii.Ubigia.Infrastructure.Hosting.Grpc.Tests
{
    using Xunit;
    using System;
    using EtAlii.Ubigia.Api.Transport.Management.Grpc;

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
