// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Tests;
    using Xunit;


    public class TemporalGraphPathWeaverTests
    {
        private readonly TestIdentifierFactory _testIdentifierFactory;

        public TemporalGraphPathWeaverTests()
        {
            _testIdentifierFactory = new TestIdentifierFactory();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void TemporalGraphPathWeaver_Create()
        {
            // Arrange.

            // Act.
            var weaver = new TemporalGraphPathWeaver();

            // Assert.
            Assert.NotNull(weaver);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void TemporalGraphPathWeaver_Weave_01()
        {
            // Arrange.
            var weaver = new TemporalGraphPathWeaver();
            var start = _testIdentifierFactory.Create();
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
            var start = _testIdentifierFactory.Create();
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
            Assert.Equal(12, result.Length);
            Assert.Equal(path[0], result[0]);
            Assert.Equal(path[1], result[2]);
            Assert.Equal(path[2], result[4]);
            Assert.Equal(path[3], result[6]);
            Assert.Equal(path[4], result[8]);
            Assert.Equal(path[5], result[9]);

            Assert.Equal(path[6], result[10]);
            Assert.Equal(path[7], result[11]);

            Assert.Equal(GraphRelation.Final, result[1]);
            Assert.Equal(GraphRelation.Final, result[3]);
            Assert.Equal(GraphRelation.Final, result[5]);
            Assert.Equal(GraphRelation.Final, result[7]);
            Assert.Equal(GraphRelation.Downdate, result[9]);
            Assert.Equal(GraphRelation.Parent, result[10]);
            Assert.IsType<GraphNode>(result[11]);
            Assert.Equal("John",result[11].ToString());
        }
    }
}
