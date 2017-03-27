namespace EtAlii.Ubigia.Api.Tests.UnitTests
{
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Storage;
    using EtAlii.Ubigia.Storage.Tests;
    using Xunit;
    using TestAssembly = EtAlii.Ubigia.Api.Tests.TestAssembly;

    
    public class TemporalGraphPathWeaver_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void TemporalGraphPathWeaver_Create()
        {
            // Arrange.

            // Act.
            var weaver = new TemporalGraphPathWeaver();

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void TemporalGraphPathWeaver_Weave_01()
        {
            // Arrange.
            var weaver = new TemporalGraphPathWeaver();
            var start = TestIdentifier.Create();
            var path = GraphPath.Create(
                start,
                GraphRelation.Parent,
                new GraphNode("Persons"),
                GraphRelation.Parent,
                new GraphNode("Doe"),
                GraphRelation.Parent,
                new GraphNode("John"));

            // Act.
            var result = weaver.Weave(path);

            // Assert.
            Assert.Equal(14, result.Length);
            Assert.Equal(path[0], result[0]);
            Assert.Equal(path[1], result[2]);
            Assert.Equal(path[2], result[4]);
            Assert.Equal(path[3], result[6]);
            Assert.Equal(path[4], result[8]);
            Assert.Equal(path[5], result[10]);
            Assert.Equal(path[6], result[12]);

            Assert.Equal(GraphRelation.Final, result[1]);
            Assert.Equal(GraphRelation.Final, result[3]);
            Assert.Equal(GraphRelation.Final, result[5]);
            Assert.Equal(GraphRelation.Final, result[7]);
            Assert.Equal(GraphRelation.Final, result[9]);
            Assert.Equal(GraphRelation.Final, result[11]);
            Assert.Equal(GraphRelation.Final, result[13]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void TemporalGraphPathWeaver_Weave_02()
        {
            // Arrange.
            var weaver = new TemporalGraphPathWeaver();
            var start = TestIdentifier.Create();
            var path = GraphPath.Create(
                start,
                GraphRelation.Parent,
                new GraphNode("Persons"),
                GraphRelation.Parent,
                new GraphNode("Doe"),
                GraphRelation.Downdate,
                GraphRelation.Parent,
                new GraphNode("John"));

            // Act.
            var result = weaver.Weave(path);

            // Assert.
            Assert.Equal(14, result.Length);
            Assert.Equal(path[0], result[0]);
            Assert.Equal(path[1], result[2]);
            Assert.Equal(path[2], result[4]);
            Assert.Equal(path[3], result[6]);
            Assert.Equal(path[4], result[8]);
            Assert.Equal(path[5], result[9]);
            Assert.Equal(path[6], result[10]);
            Assert.Equal(path[7], result[12]);

            Assert.Equal(GraphRelation.Final, result[1]);
            Assert.Equal(GraphRelation.Final, result[3]);
            Assert.Equal(GraphRelation.Final, result[5]);
            Assert.Equal(GraphRelation.Final, result[7]);
            Assert.Equal(GraphRelation.Final, result[11]);
            Assert.Equal(GraphRelation.Final, result[13]);
        }
    }
}
