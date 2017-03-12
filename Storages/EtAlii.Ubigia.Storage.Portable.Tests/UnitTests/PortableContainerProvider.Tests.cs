namespace EtAlii.Ubigia.Storage.Portable.Tests.UnitTests
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Storage.Portable;
    using EtAlii.Ubigia.Storage.Tests;
    using Xunit;
    using TestAssembly = EtAlii.Ubigia.Storage.Portable.Tests.TestAssembly;

    
    public class PortableContainerProvider_Tests
    {
        private readonly IContainerProvider _containerProvider = new PortableContainerProvider();

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PortableContainerProvider_FromIds()
        {
            // Arrange.
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();

            // Act.
            var containerIdentifier = _containerProvider.ForEntry(storage, account, space);

            // Assert.
            Assert.Equal("Entries", containerIdentifier.Paths[0]);
            Assert.Equal(Base36Convert.ToString(storage), containerIdentifier.Paths[1]);
            Assert.Equal(Base36Convert.ToString(account), containerIdentifier.Paths[2]);
            Assert.Equal(Base36Convert.ToString(space), containerIdentifier.Paths[3]);
            Assert.Equal(4, containerIdentifier.Paths.Length);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PortableContainerProvider_FromIdentifier()
        {
            // Arrange.
            var id = TestIdentifier.Create();

            // Act.
            var containerIdentifier = _containerProvider.FromIdentifier(id);

            // Assert.
            Assert.Equal("Entries", containerIdentifier.Paths[0]);
            Assert.Equal(Base36Convert.ToString(id.Storage), containerIdentifier.Paths[1]);
            Assert.Equal(Base36Convert.ToString(id.Account), containerIdentifier.Paths[2]);
            Assert.Equal(Base36Convert.ToString(id.Space), containerIdentifier.Paths[3]);
            Assert.Equal(Base36Convert.ToString(id.Era), containerIdentifier.Paths[4]);
            Assert.Equal(Base36Convert.ToString(id.Period), containerIdentifier.Paths[5]);
            Assert.Equal(Base36Convert.ToString(id.Moment), containerIdentifier.Paths[6]);
            Assert.Equal(7, containerIdentifier.Paths.Length);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PortableContainerProvider_NotEquals_True()
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
        public void PortableContainerProvider_NotEquals_False()
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
        public void PortableContainerProvider_Equals_True()
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
        public void PortableContainerProvider_Equals_False()
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
        public void PortableContainerProvider_FromIdentifier_TrimTime_True()
        {
            // Arrange.
            var id = TestIdentifier.Create();

            // Act.
            var containerIdentifier = _containerProvider.FromIdentifier(id, true);

            // Assert.
            Assert.Equal("Entries", containerIdentifier.Paths[0]);
            Assert.Equal(Base36Convert.ToString(id.Storage), containerIdentifier.Paths[1]);
            Assert.Equal(Base36Convert.ToString(id.Account), containerIdentifier.Paths[2]);
            Assert.Equal(Base36Convert.ToString(id.Space), containerIdentifier.Paths[3]);
            Assert.Equal(4, containerIdentifier.Paths.Length);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PortableContainerProvider_FromIdentifier_TrimTime_False()
        {
            // Arrange.
            var id = TestIdentifier.Create();

            // Act.
            var containerIdentifier = _containerProvider.FromIdentifier(id, false);

            // Assert.
            Assert.Equal("Entries", containerIdentifier.Paths[0]);
            Assert.Equal(Base36Convert.ToString(id.Storage), containerIdentifier.Paths[1]);
            Assert.Equal(Base36Convert.ToString(id.Account), containerIdentifier.Paths[2]);
            Assert.Equal(Base36Convert.ToString(id.Space), containerIdentifier.Paths[3]);
            Assert.Equal(Base36Convert.ToString(id.Era), containerIdentifier.Paths[4]);
            Assert.Equal(Base36Convert.ToString(id.Period), containerIdentifier.Paths[5]);
            Assert.Equal(Base36Convert.ToString(id.Moment), containerIdentifier.Paths[6]);
            Assert.Equal(7, containerIdentifier.Paths.Length);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PortableContainerProvider_FromEmptyPaths_ToString()
        {
            // Arrange.

            // Act.
            var containerId = ContainerIdentifier.FromPaths();

            // Assert.
            Assert.NotNull(containerId);
            Assert.Equal(String.Format("{0}.Empty", typeof(ContainerIdentifier).Name), containerId.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PortableContainerProvider_Single_Id_ToString()
        {
            // Arrange.
            var id = Guid.NewGuid().ToString();

            // Act.
            var containerId = ContainerIdentifier.FromPaths(id);

            // Assert.
            Assert.NotNull(containerId);
            Assert.Equal(id, containerId.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PortableContainerProvider_Multiple_Ids_ToString()
        {
            // Arrange.
            var first = Guid.NewGuid().ToString();
            var second = Guid.NewGuid().ToString();

            // Act.
            var containerId = ContainerIdentifier.FromPaths(first, second);

            // Assert.
            Assert.NotNull(containerId);
            Assert.Equal(String.Join("\\", first, second), containerId.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PortableContainerProvider_Paths()
        {
            // Arrange.
            var first = Guid.NewGuid().ToString();
            var second = Guid.NewGuid().ToString();

            // Act.
            var containerId = ContainerIdentifier.FromPaths(first, second);

            // Assert.
            Assert.NotNull(containerId);
            Assert.Equal(first, containerId.Paths[0]);
            Assert.Equal(second, containerId.Paths[1]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PortableContainerProvider_Combine()
        {
            // Arrange.
            var first = Guid.NewGuid().ToString();
            var second = Guid.NewGuid().ToString();
            var containerId = ContainerIdentifier.FromPaths(first);

            // Act.
            containerId = ContainerIdentifier.Combine(containerId, second);

            // Assert.
            Assert.NotNull(containerId);
            Assert.Equal(String.Join("\\", first, second), containerId.ToString());
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void PortableContainerProvider_Empty_ToString()
        {
            // Arrange.

            // Act.
            var result = ContainerIdentifier.Empty.ToString();

            // Assert.
            Assert.Equal("ContainerIdentifier.Empty", result);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void PortableContainerProvider_Comparison_With_Right_Null()
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
        public void PortableContainerProvider_Comparison_With_Self()
        {
            // Arrange.
            var containerProvider = new PortableContainerProvider();
            var id = TestIdentifier.Create();
            var first = _containerProvider.FromIdentifier(id);
            var second = first as object;

            // Act.
            var result = first.Equals(second);

            // Assert.
            Assert.True(result, "A PortableContainerProvider generated ContainerIdentifier should also match with itselve wrapped as object.");
        }


    }

}
