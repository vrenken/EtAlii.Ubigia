namespace EtAlii.Ubigia.Api.Tests.UnitTests
{
    using System;
    using Xunit;

    public class Relation_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void Relation_ToString()
        {
            // Arrange.
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            UInt64 era = 0;
            UInt64 period = 2;
            UInt64 moment = 3;
            var identifier = Identifier.Create(storage, account, space, era, period, moment);
            var expectedResult = $"{identifier.ToLocationString()}/{identifier.ToTimeString()} (1)";
            // Act.
            var result = Relation.NewRelation(identifier).ToString();

            // Assert.
            Assert.Equal(expectedResult, result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
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
