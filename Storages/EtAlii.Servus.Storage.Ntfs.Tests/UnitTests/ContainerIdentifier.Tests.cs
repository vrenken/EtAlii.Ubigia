namespace EtAlii.Servus.Storage.Ntfs.Tests.UnitTests
{
    using EtAlii.Servus.Storage.Ntfs.Tests;
    using EtAlii.Servus.Storage;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class ContainerIdentifier_Tests : NtfsStorageTestBase
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContainerIdentifier_Empty_ToString()
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
    }
}
