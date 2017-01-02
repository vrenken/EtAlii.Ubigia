namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api.Data.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Content = EtAlii.Servus.Api.Content;

    [TestClass]
    public class GraphPathBuilder_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void GraphPathBuilder_Create()
        {
            // Arrange.

            // Act.
            var builder = new GraphPathBuilder();

            // Assert.
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void GraphPathBuilder_Add_Node()
        {
            // Arrange.
            var builder = new GraphPathBuilder();

            // Act.
            builder.Add("First");

            // Assert.
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void GraphPathBuilder_Add_Relation()
        {
            // Arrange.
            var builder = new GraphPathBuilder();

            // Act.
            builder.Add(GraphRelation.Child);

            // Assert.
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void GraphPathBuilder_Add_Node_ToPath()
        {
            // Arrange.
            var builder = new GraphPathBuilder();
            builder.Add("First");

            // Act.
            var path = builder.ToPath();

            // Assert.
            Assert.AreEqual(1, path.Parts.Length);
            Assert.IsInstanceOfType(path.Parts[0], typeof(GraphNode));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void GraphPathBuilder_Add_Relation_ToPath()
        {
            // Arrange.
            var builder = new GraphPathBuilder();
            builder.Add(GraphRelation.Child);

            // Act.
            var path = builder.ToPath();

            // Assert.
            Assert.AreEqual(1, path.Parts.Length);
            Assert.IsInstanceOfType(path.Parts[0], typeof(GraphRelation));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void GraphPathBuilder_Add_Node_And_Relation_ToPath()
        {
            // Arrange.
            var builder = new GraphPathBuilder();
            builder.Add("First");
            builder.Add(GraphRelation.Child);

            // Act.
            var path = builder.ToPath();

            // Assert.
            Assert.AreEqual(2, path.Parts.Length);
            Assert.IsInstanceOfType(path.Parts[0], typeof(GraphNode));
            Assert.IsInstanceOfType(path.Parts[1], typeof(GraphRelation));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void GraphPathBuilder_Add_Node_And_Relation_Clear()
        {
            // Arrange.
            var builder = new GraphPathBuilder();
            builder.Add("First");
            builder.Add(GraphRelation.Child);

            // Act.
            builder.Clear();

            // Assert.
            var path = builder.ToPath();
            Assert.AreEqual(0, path.Parts.Length);
        }
    }
}
