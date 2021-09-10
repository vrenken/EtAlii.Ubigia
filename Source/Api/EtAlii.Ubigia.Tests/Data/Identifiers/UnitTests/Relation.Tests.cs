namespace EtAlii.Ubigia.Tests
{
    using System;
    using Xunit;

    public class RelationTests
    {
        [Fact]
        public void Relation_ToString()
        {
            // Arrange.
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            ulong era = 0;
            ulong period = 2;
            ulong moment = 3;
            ulong relationMoment = 5;
            var identifier = Identifier.Create(storage, account, space, era, period, moment);
            var expectedResult = $"{identifier.ToLocationString()}/{identifier.ToTimeString()} ({relationMoment})";
            // Act.
            var result = Relation.Create(identifier, relationMoment).ToString();

            // Assert.
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Relation_Equal_01_Same()
        {
            // Arrange.
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            ulong era = 0;
            ulong period = 2;
            ulong moment = 3;
            var identifier = Identifier.Create(storage, account, space, era, period, moment);
            var relation1 = Relation.Create(identifier, 0);
            var relation2 = Relation.Create(identifier, 0);

            // Act.
            var areEqual = relation1.Equals(relation2);

            // Assert.
            Assert.True(areEqual);
        }

        [Fact]
        public void Relation_Equal_02_Same()
        {
            // Arrange.
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            ulong era = 0;
            ulong period = 2;
            ulong moment = 3;
            var identifier = Identifier.Create(storage, account, space, era, period, moment);
            var relation1 = Relation.Create(identifier, 0);
            var relation2 = Relation.Create(identifier, 0);

            // Act.
            var areEqual = relation1.Equals((object)relation2);

            // Assert.
            Assert.True(areEqual);
        }

        [Fact]
        public void Relation_Equal_03_Same()
        {
            // Arrange.
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            ulong era = 0;
            ulong period = 2;
            ulong moment = 3;
            var identifier = Identifier.Create(storage, account, space, era, period, moment);
            var relation1 = Relation.Create(identifier, 0);
            var relation2 = Relation.Create(identifier, 0);

            // Act.
            var areEqual = relation1 == relation2;

            // Assert.
            Assert.True(areEqual);
        }

        [Fact]
        public void Relation_Equal_04_Differ()
        {
            // Arrange.
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            ulong era = 0;
            ulong period = 2;
            ulong moment1 = 3;
            ulong moment2 = 4;
            var identifier1 = Identifier.Create(storage, account, space, era, period, moment1);
            var identifier2 = Identifier.Create(storage, account, space, era, period, moment2);
            var relation1 = Relation.Create(identifier1, 0);
            var relation2 = Relation.Create(identifier2, 0);

            // Act.
            var areEqual = relation1.Equals(relation2);

            // Assert.
            Assert.False(areEqual);
        }

        [Fact]
        public void Relation_Equal_05_Differ()
        {
            // Arrange.
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            ulong era = 0;
            ulong period = 2;
            ulong moment1 = 3;
            ulong moment2 = 4;
            var identifier1 = Identifier.Create(storage, account, space, era, period, moment1);
            var identifier2 = Identifier.Create(storage, account, space, era, period, moment2);
            var relation1 = Relation.Create(identifier1, 0);
            var relation2 = Relation.Create(identifier2, 0);

            // Act.
            var areEqual = relation1.Equals((object)relation2);

            // Assert.
            Assert.False(areEqual);
        }

        [Fact]
        public void Relation_Equal_06_Differ()
        {
            // Arrange.
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            ulong era = 0;
            ulong period = 2;
            ulong moment1 = 3;
            ulong moment2 = 4;
            var identifier1 = Identifier.Create(storage, account, space, era, period, moment1);
            var identifier2 = Identifier.Create(storage, account, space, era, period, moment2);
            var relation1 = Relation.Create(identifier1, 0);
            var relation2 = Relation.Create(identifier2, 0);

            // Act.
            var areEqual = relation1 == relation2;

            // Assert.
            Assert.False(areEqual);
        }


        [Fact]
        public void Relation_Equal_07_Differ_Null()
        {
            // Arrange.
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            ulong era = 0;
            ulong period = 2;
            ulong moment = 3;
            var identifier = Identifier.Create(storage, account, space, era, period, moment);
            var relation1 = Relation.Create(identifier, 0);

            // Act.
            var areEqual = relation1.Equals(null);

            // Assert.
            Assert.False(areEqual);
        }

        [Fact]
        public void Relation_Equal_08_Differ_Null()
        {
            // Arrange.
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            ulong era = 0;
            ulong period = 2;
            ulong moment = 3;
            var identifier = Identifier.Create(storage, account, space, era, period, moment);
            var relation1 = Relation.Create(identifier, 0);

            // Act.
            var areEqual = relation1.Equals(null);

            // Assert.
            Assert.False(areEqual);
        }

        [Fact]
        public void Relation_Equal_09_Differ_Null()
        {
            // Arrange.
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            ulong era = 0;
            ulong period = 2;
            ulong moment = 3;
            var identifier = Identifier.Create(storage, account, space, era, period, moment);
            var relation1 = Relation.Create(identifier, 0);

            // Act.
            var areEqual = relation1 == Relation.None;

            // Assert.
            Assert.False(areEqual);
        }

        [Fact]
        public void Relation_None_ToString()
        {
            // Arrange.

            // Act.
            var result = Relation.None.ToString();

            // Assert.
            Assert.Equal("Relation.None", result);
        }
    }
}
