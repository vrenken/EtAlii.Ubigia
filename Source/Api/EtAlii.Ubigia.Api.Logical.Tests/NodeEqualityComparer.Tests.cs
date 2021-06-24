// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    public class NodeEqualityComparerTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void NodeEqualityComparer_Create()
        {
            // Arrange.

            // Act.
            var comparer = new NodeEqualityComparer();

            // Assert.
            Assert.NotNull(comparer);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NodeEqualityComparer_Default()
        {
            // Arrange.

            // Act.
            var comparer = NodeEqualityComparer.Default;

            // Assert.
            Assert.NotNull(comparer);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void NodeEqualityComparer_Compare_Null_00()
        {
            // Arrange.
            var comparer = NodeEqualityComparer.Default;
            var first = (Node)null;
            var second = (Node)null;

            // Act.
            var equal = comparer.Equals(first, second);

            // Assert.
            Assert.True(equal);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NodeEqualityComparer_Compare_Null_01()
        {
            // Arrange.
            var testIdentifierFactory = new TestIdentifierFactory();

            var comparer = NodeEqualityComparer.Default;
            var first = new Node(Entry.NewEntry(testIdentifierFactory.Create()));
            var second = new Node(Entry.NewEntry(testIdentifierFactory.Create()));

            // Act.
            var equal = comparer.Equals(first, second);

            // Assert.
            Assert.False(equal);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void NodeEqualityComparer_Compare_Null_02()
        {
            // Arrange.
            var testIdentifierFactory = new TestIdentifierFactory();

            var comparer = NodeEqualityComparer.Default;
            var first = (Node)null;
            var second = new Node(Entry.NewEntry(testIdentifierFactory.Create()));

            // Act.
            var equal = comparer.Equals(first, second);

            // Assert.
            Assert.False(equal);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void NodeEqualityComparer_Compare_Null_03()
        {
            // Arrange.
            var testIdentifierFactory = new TestIdentifierFactory();

            var comparer = NodeEqualityComparer.Default;
            var first = new Node(Entry.NewEntry(testIdentifierFactory.Create()));
            var second = (Node)null;

            // Act.
            var equal = comparer.Equals(first, second);

            // Assert.
            Assert.False(equal);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NodeEqualityComparer_Compare_NotNull()
        {
            // Arrange.
            var testIdentifierFactory = new TestIdentifierFactory();
            var identifier = testIdentifierFactory.Create();

            var comparer = NodeEqualityComparer.Default;
            var first = new Node(Entry.NewEntry(identifier));
            var second = new Node(Entry.NewEntry(identifier));

            // Act.
            var equal = comparer.Equals(first, second);

            // Assert.
            Assert.True(equal);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void NodeEqualityComparer_Compare_Same_Reference_Type()
        {
            // Arrange.
            var testIdentifierFactory = new TestIdentifierFactory();
            var identifier = testIdentifierFactory.Create();

            var comparer = NodeEqualityComparer.Default;
            var first = new Node(Entry.NewEntry(identifier));

            // Act.
            var equal = comparer.Equals(first, first);

            // Assert.
            Assert.True(equal);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NodeEqualityComparer_GetHashCode()
        {
            // Arrange.
            var testIdentifierFactory = new TestIdentifierFactory();
            var identifier = testIdentifierFactory.Create();

            var comparer = NodeEqualityComparer.Default;
            var first = new Node(Entry.NewEntry(identifier));
            var second = new Node(Entry.NewEntry(identifier));

            // Act.
            var firstHash = comparer.GetHashCode(first);
            var secondHash = comparer.GetHashCode(second);

            // Assert.
            Assert.Equal(firstHash, secondHash);
        }

    }
}
