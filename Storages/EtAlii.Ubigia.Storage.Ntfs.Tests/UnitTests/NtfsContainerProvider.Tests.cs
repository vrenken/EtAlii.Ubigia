namespace EtAlii.Ubigia.Storage.Ntfs.Tests.UnitTests
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Storage.Ntfs;
    using EtAlii.Ubigia.Storage.Tests;
    using Xunit;
    using TestAssembly = EtAlii.Ubigia.Storage.Ntfs.Tests.TestAssembly;

    
    public class NtfsContainerProvider_Tests
    {
        private readonly IContainerProvider _containerProvider = new NtfsContainerProvider();

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsContainerProvider_FromIds()
        {
            // Arrange.
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();

            // Act.
            var containerIdentifier = _containerProvider.ForEntry(storage, account, space);

            // Assert.
            Assert.Equal("Entries", containerIdentifier.Paths[0]);
            Assert.Equal(storage.ToString(), containerIdentifier.Paths[1]);
            Assert.Equal(account.ToString(), containerIdentifier.Paths[2]);
            Assert.Equal(space.ToString(), containerIdentifier.Paths[3]);
            Assert.Equal(4, containerIdentifier.Paths.Length);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsContainerProvider_FromIdentifier()
        {
            // Arrange.
            var id = TestIdentifier.Create();

            // Act.
            var containerIdentifier = _containerProvider.FromIdentifier(id);

            // Assert.
            Assert.Equal("Entries", containerIdentifier.Paths[0]);
            Assert.Equal(id.Storage.ToString(), containerIdentifier.Paths[1]);
            Assert.Equal(id.Account.ToString(), containerIdentifier.Paths[2]);
            Assert.Equal(id.Space.ToString(), containerIdentifier.Paths[3]);
            Assert.Equal(id.Era.ToString(), containerIdentifier.Paths[4]);
            Assert.Equal(id.Period.ToString(), containerIdentifier.Paths[5]);
            Assert.Equal(id.Moment.ToString(), containerIdentifier.Paths[6]);
            Assert.Equal(7, containerIdentifier.Paths.Length);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsContainerProvider_NotEquals_True()
        {
            // Arrange.
            var firstId = TestIdentifier.Create();
            var secondId = TestIdentifier.Create();

            // Act.
            var firstContainerIdentifier = _containerProvider.FromIdentifier(firstId);
            var secondContainerIdentifier = _containerProvider.FromIdentifier(secondId);

            // Assert.
            Assert.True(firstContainerIdentifier != secondContainerIdentifier);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsContainerProvider_NotEquals_False()
        {
            // Arrange.
            var firstId = TestIdentifier.Create();
            var secondId = Identifier.Create(firstId.Storage, firstId.Account, firstId.Space, firstId.Era, firstId.Period, firstId.Moment);

            // Act.
            var firstContainerIdentifier = _containerProvider.FromIdentifier(firstId);
            var secondContainerIdentifier = _containerProvider.FromIdentifier(secondId);

            // Assert.
            Assert.False(firstContainerIdentifier != secondContainerIdentifier);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsContainerProvider_Equals_True()
        {
            // Arrange.
            var firstId = TestIdentifier.Create();
            var secondId = Identifier.Create(firstId.Storage, firstId.Account, firstId.Space, firstId.Era, firstId.Period, firstId.Moment);

            // Act.
            var firstContainerIdentifier = _containerProvider.FromIdentifier(firstId);
            var secondContainerIdentifier = _containerProvider.FromIdentifier(secondId);

            // Assert.
            Assert.True(firstContainerIdentifier == secondContainerIdentifier);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsContainerProvider_Equals_False()
        {
            // Arrange.
            var firstId = TestIdentifier.Create();
            var secondId = TestIdentifier.Create();

            // Act.
            var firstContainerIdentifier = _containerProvider.FromIdentifier(firstId);
            var secondContainerIdentifier = _containerProvider.FromIdentifier(secondId);

            // Assert.
            Assert.False(firstContainerIdentifier == secondContainerIdentifier);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsContainerProvider_FromIdentifier_TrimTime_True()
        {
            // Arrange.
            var id = TestIdentifier.Create();

            // Act.
            var containerIdentifier = _containerProvider.FromIdentifier(id, true);

            // Assert.
            Assert.Equal("Entries", containerIdentifier.Paths[0]);
            Assert.Equal(id.Storage.ToString(), containerIdentifier.Paths[1]);
            Assert.Equal(id.Account.ToString(), containerIdentifier.Paths[2]);
            Assert.Equal(id.Space.ToString(), containerIdentifier.Paths[3]);
            Assert.Equal(4, containerIdentifier.Paths.Length);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsContainerProvider_FromIdentifier_TrimTime_False()
        {
            // Arrange.
            var id = TestIdentifier.Create();

            // Act.
            var containerIdentifier = _containerProvider.FromIdentifier(id, false);

            // Assert.
            Assert.Equal("Entries", containerIdentifier.Paths[0]);
            Assert.Equal(id.Storage.ToString(), containerIdentifier.Paths[1]);
            Assert.Equal(id.Account.ToString(), containerIdentifier.Paths[2]);
            Assert.Equal(id.Space.ToString(), containerIdentifier.Paths[3]);
            Assert.Equal(id.Era.ToString(), containerIdentifier.Paths[4]);
            Assert.Equal(id.Period.ToString(), containerIdentifier.Paths[5]);
            Assert.Equal(id.Moment.ToString(), containerIdentifier.Paths[6]);
            Assert.Equal(7, containerIdentifier.Paths.Length);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsContainerProvider_FromEmptyPaths_ToString()
        {
            // Arrange.

            // Act.
            var containerId = ContainerIdentifier.FromPaths();

            // Assert.
            Assert.NotNull(containerId);
            Assert.Equal(String.Format("{0}.Empty", typeof(ContainerIdentifier).Name), containerId.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsContainerProvider_Single_Id_ToString()
        {
            // Arrange.
            var id = Guid.NewGuid().ToString();

            // Act.
            var containerId = ContainerIdentifier.FromPaths(id.ToString());

            // Assert.
            Assert.NotNull(containerId);
            Assert.Equal(id.ToString(), containerId.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsContainerProvider_Multiple_Ids_ToString()
        {
            // Arrange.
            var first = Guid.NewGuid().ToString();
            var second = Guid.NewGuid().ToString();

            // Act.
            var containerId = ContainerIdentifier.FromPaths(first.ToString(), second.ToString());

            // Assert.
            Assert.NotNull(containerId);
            Assert.Equal(String.Join("\\", first.ToString(), second.ToString()), containerId.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsContainerProvider_Paths()
        {
            // Arrange.
            var first = Guid.NewGuid().ToString();
            var second = Guid.NewGuid().ToString();

            // Act.
            var containerId = ContainerIdentifier.FromPaths(first.ToString(), second.ToString());

            // Assert.
            Assert.NotNull(containerId);
            Assert.Equal(first, containerId.Paths[0]);
            Assert.Equal(second, containerId.Paths[1]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsContainerProvider_Combine()
        {
            // Arrange.
            var first = Guid.NewGuid().ToString();
            var second = Guid.NewGuid().ToString();
            var containerId = ContainerIdentifier.FromPaths(first.ToString());

            // Act.
            containerId = ContainerIdentifier.Combine(containerId, second);

            // Assert.
            Assert.NotNull(containerId);
            Assert.Equal(String.Join("\\", first.ToString(), second.ToString()), containerId.ToString());
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsContainerProvider_Empty_ToString()
        {
            // Arrange.

            // Act.
            var result = ContainerIdentifier.Empty.ToString();

            // Assert.
            Assert.Equal("ContainerIdentifier.Empty", result);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsContainerProvider_Comparison_With_Right_Null()
        {
            // Arrange.
            var id = TestIdentifier.Create();
            var first = _containerProvider.FromIdentifier(id);
            var second = (object)null;

            // Act.
            var result = first.Equals(second);

            // Assert.
            Assert.False(result, "A ContainerIdentifier should not match with null");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsContainerProvider_Comparison_With_Self()
        {
            // Arrange.
            var containerProvider = new NtfsContainerProvider();
            var id = TestIdentifier.Create();
            var first = _containerProvider.FromIdentifier(id);
            var second = first as object;

            // Act.
            var result = first.Equals(second);

            // Assert.
            Assert.True(result, "A NtfsContainerProvider generated ContainerIdentifier should also match with itselve wrapped as object.");
        }


    }

}
