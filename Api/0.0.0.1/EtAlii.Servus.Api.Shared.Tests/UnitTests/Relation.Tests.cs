namespace EtAlii.Servus.Api.Tests.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class Relation_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
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
            var expectedResult = String.Format("{0}/{1} (1)", identifier.ToLocationString(), identifier.ToTimeString());
            // Act.
            var result = Relation.NewRelation(identifier).ToString();

            // Assert.
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Relation_None_ToString()
        {
            // Arrange.

            // Act.
            var result = Relation.None.ToString();

            // Assert.
            Assert.AreEqual("Relation.None", result);
        }
    }
}
