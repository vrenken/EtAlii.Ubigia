
namespace EtAlii.Servus.Storage.Tests.UnitTests
{
    using System;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Fabric;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ContainerIdentifier_Tests
    {
        private readonly IContainers _containers = new Containers();

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContainerIdentifier_FromIds()
        {
            // Arrange.
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();

            // Act.
            var containerIdentifier = _containers.ForEntry(storage, account, space);

            // Assert.
            Assert.AreEqual("Entries", containerIdentifier.Paths[0]);
            Assert.AreEqual(Base36Convert.ToString(storage), containerIdentifier.Paths[1]);
            Assert.AreEqual(Base36Convert.ToString(account), containerIdentifier.Paths[2]);
            Assert.AreEqual(Base36Convert.ToString(space), containerIdentifier.Paths[3]);
            Assert.AreEqual(4, containerIdentifier.Paths.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContainerIdentifier_FromIdentifier()
        {
            // Arrange.
            var id = TestIdentifier.Create();

            // Act.
            var containerIdentifier = _containers.FromIdentifier(id);

            // Assert.
            Assert.AreEqual("Entries", containerIdentifier.Paths[0]);
            Assert.AreEqual(Base36Convert.ToString(id.Storage), containerIdentifier.Paths[1]);
            Assert.AreEqual(Base36Convert.ToString(id.Account), containerIdentifier.Paths[2]);
            Assert.AreEqual(Base36Convert.ToString(id.Space), containerIdentifier.Paths[3]);
            Assert.AreEqual(Base36Convert.ToString(id.Era), containerIdentifier.Paths[4]);
            Assert.AreEqual(Base36Convert.ToString(id.Period), containerIdentifier.Paths[5]);
            Assert.AreEqual(Base36Convert.ToString(id.Moment), containerIdentifier.Paths[6]);
            Assert.AreEqual(7, containerIdentifier.Paths.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContainerIdentifier_NotEquals_True()
        {
            // Arrange.
            var firstId = TestIdentifier.Create();
            var secondId = TestIdentifier.Create();

            // Act.
            var firstContainerIdentifier = _containers.FromIdentifier(firstId);
            var secondContainerIdentifier = _containers.FromIdentifier(secondId);

            // Assert.
            Assert.IsTrue(firstContainerIdentifier != secondContainerIdentifier);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContainerIdentifier_NotEquals_False()
        {
            // Arrange.
            var firstId = TestIdentifier.Create();
            var secondId = Identifier.Create(firstId.Storage, firstId.Account, firstId.Space, firstId.Era, firstId.Period, firstId.Moment);

            // Act.
            var firstContainerIdentifier = _containers.FromIdentifier(firstId);
            var secondContainerIdentifier = _containers.FromIdentifier(secondId);

            // Assert.
            Assert.IsFalse(firstContainerIdentifier != secondContainerIdentifier);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContainerIdentifier_Equals_True()
        {
            // Arrange.
            var firstId = TestIdentifier.Create();
            var secondId = Identifier.Create(firstId.Storage, firstId.Account, firstId.Space, firstId.Era, firstId.Period, firstId.Moment);

            // Act.
            var firstContainerIdentifier = _containers.FromIdentifier(firstId);
            var secondContainerIdentifier = _containers.FromIdentifier(secondId);

            // Assert.
            Assert.IsTrue(firstContainerIdentifier == secondContainerIdentifier);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContainerIdentifier_Equals_False()
        {
            // Arrange.
            var firstId = TestIdentifier.Create();
            var secondId = TestIdentifier.Create();

            // Act.
            var firstContainerIdentifier = _containers.FromIdentifier(firstId);
            var secondContainerIdentifier = _containers.FromIdentifier(secondId);

            // Assert.
            Assert.IsFalse(firstContainerIdentifier == secondContainerIdentifier);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContainerIdentifier_FromIdentifier_TrimTime_True()
        {
            // Arrange.
            var id = TestIdentifier.Create();

            // Act.
            var containerIdentifier = _containers.FromIdentifier(id, true);

            // Assert.
            Assert.AreEqual("Entries", containerIdentifier.Paths[0]);
            Assert.AreEqual(Base36Convert.ToString(id.Storage), containerIdentifier.Paths[1]);
            Assert.AreEqual(Base36Convert.ToString(id.Account), containerIdentifier.Paths[2]);
            Assert.AreEqual(Base36Convert.ToString(id.Space), containerIdentifier.Paths[3]);
            Assert.AreEqual(4, containerIdentifier.Paths.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContainerIdentifier_FromIdentifier_TrimTime_False()
        {
            // Arrange.
            var id = TestIdentifier.Create();

            // Act.
            var containerIdentifier = _containers.FromIdentifier(id, false);

            // Assert.
            Assert.AreEqual("Entries", containerIdentifier.Paths[0]);
            Assert.AreEqual(Base36Convert.ToString(id.Storage), containerIdentifier.Paths[1]);
            Assert.AreEqual(Base36Convert.ToString(id.Account), containerIdentifier.Paths[2]);
            Assert.AreEqual(Base36Convert.ToString(id.Space), containerIdentifier.Paths[3]);
            Assert.AreEqual(Base36Convert.ToString(id.Era), containerIdentifier.Paths[4]);
            Assert.AreEqual(Base36Convert.ToString(id.Period), containerIdentifier.Paths[5]);
            Assert.AreEqual(Base36Convert.ToString(id.Moment), containerIdentifier.Paths[6]);
            Assert.AreEqual(7, containerIdentifier.Paths.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContainerIdentifier_FromEmptyPaths_ToString()
        {
            // Arrange.

            // Act.
            var containerId = ContainerIdentifier.FromPaths();

            // Assert.
            Assert.IsNotNull(containerId);
            Assert.AreEqual(String.Format("{0}.Empty", typeof(ContainerIdentifier).Name), containerId.ToString());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContainerIdentifier_Single_Id_ToString()
        {
            // Arrange.
            var id = Guid.NewGuid().ToString();

            // Act.
            var containerId = ContainerIdentifier.FromPaths(id.ToString());

            // Assert.
            Assert.IsNotNull(containerId);
            Assert.AreEqual(id.ToString(), containerId.ToString());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContainerIdentifier_Multiple_Ids_ToString()
        {
            // Arrange.
            var first = Guid.NewGuid().ToString();
            var second = Guid.NewGuid().ToString();

            // Act.
            var containerId = ContainerIdentifier.FromPaths(first.ToString(), second.ToString());

            // Assert.
            Assert.IsNotNull(containerId);
            Assert.AreEqual(String.Join("\\", first.ToString(), second.ToString()), containerId.ToString());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContainerIdentifier_Paths()
        {
            // Arrange.
            var first = Guid.NewGuid().ToString();
            var second = Guid.NewGuid().ToString();

            // Act.
            var containerId = ContainerIdentifier.FromPaths(first.ToString(), second.ToString());

            // Assert.
            Assert.IsNotNull(containerId);
            Assert.AreEqual(first, containerId.Paths[0]);
            Assert.AreEqual(second, containerId.Paths[1]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContainerIdentifier_Combine()
        {
            // Arrange.
            var first = Guid.NewGuid().ToString();
            var second = Guid.NewGuid().ToString();
            var containerId = ContainerIdentifier.FromPaths(first.ToString());

            // Act.
            containerId = ContainerIdentifier.Combine(containerId, second);

            // Assert.
            Assert.IsNotNull(containerId);
            Assert.AreEqual(String.Join("\\", first.ToString(), second.ToString()), containerId.ToString());
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContainerIdentifier_Empty_ToString()
        {
            // Arrange.

            // Act.
            var result = ContainerIdentifier.Empty.ToString();

            // Assert.
            Assert.AreEqual("ContainerIdentifier.Empty", result);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContainerIdentifier_Comparison_With_Right_Null()
        {
            // Arrange.
            var id = TestIdentifier.Create();
            var first = _containers.FromIdentifier(id);
            var second = (object)null;

            // Act.
            var result = first.Equals(second);

            // Assert.
            Assert.IsFalse(result, "A ContainerIdentifier should not match with null");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContainerIdentifier_Comparison_With_Self()
        {
            // Arrange.
            var containers = new Containers();
            var id = TestIdentifier.Create();
            var first = _containers.FromIdentifier(id);
            var second = first as object;

            // Act.
            var result = first.Equals(second);

            // Assert.
            Assert.IsTrue(result, "A ContainerIdentifier should also match with itselve wrapped as object.");
        }


    }

}
