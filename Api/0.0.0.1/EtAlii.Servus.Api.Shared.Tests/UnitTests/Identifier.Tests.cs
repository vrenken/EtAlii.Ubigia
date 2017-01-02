namespace EtAlii.Servus.Api.Tests.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class Identifier_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Identifier_Empty_DefaultValues()
        {
            Assert.AreEqual(Guid.Empty, Identifier.Empty.Storage, "The Identifier.Empty.Storage property is incorrect");
            Assert.AreEqual(Guid.Empty, Identifier.Empty.Account, "The Identifier.Empty.Account property is incorrect");
            Assert.AreEqual(Guid.Empty, Identifier.Empty.Space, "The Identifier.Empty.Space property is incorrect");
            Assert.AreEqual(UInt64.MinValue, Identifier.Empty.Period, "The Identifier.Empty.Period property is incorrect");
            Assert.AreEqual(UInt64.MinValue, Identifier.Empty.Moment, "The Identifier.Empty.Moment property is incorrect");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Identifier_CreateRoot()
        {
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            var identifier = Identifier.NewIdentifier(storage, account, space);

            Assert.AreEqual(storage, identifier.Storage, "The identifier.Storage property is incorrect");
            Assert.AreEqual(account, identifier.Account, "The identifier.Account property is incorrect");
            Assert.AreEqual(space, identifier.Space, "The identifier.Space property is incorrect");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Identifier_Create_With_Storage_Account_Space_Period_Moment()
        {
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            const ulong era = 0;
            const ulong period = 2;
            const ulong moment = 3;
            var identifier = Identifier.Create(storage, account, space, era, period, moment);

            Assert.AreEqual(storage, identifier.Storage, "The identifier.Storage property is incorrect");
            Assert.AreEqual(account, identifier.Account, "The identifier.Account property is incorrect");
            Assert.AreEqual(space, identifier.Space, "The identifier.Space property is incorrect");
            Assert.AreEqual(period, identifier.Period, "The identifier.Period property is incorrect");
            Assert.AreEqual(moment, identifier.Moment, "The identifier.Moment property is incorrect");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Identifier_Empty_ToString()
        {
            // Arrange.

            // Act.
            var result = Identifier.Empty.ToString();

            // Assert.
            Assert.AreEqual("Identifier.Empty", result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void IEditableIdentifier_Change()
        {
            // Arrange.
            var storage1 = Guid.NewGuid();
            var account1 = Guid.NewGuid();
            var space1 = Guid.NewGuid();
            const ulong era1 = 1;
            const ulong period1 = 2;
            const ulong moment1 = 3;

            var storage2 = Guid.NewGuid();
            var account2 = Guid.NewGuid();
            var space2 = Guid.NewGuid();
            const ulong era2 = 4;
            const ulong period2 = 5;
            const ulong moment2 = 6;
            var identifier = (IEditableIdentifier)Identifier.Create(storage1, account1, space1, era1, period1, moment1);

            // Act.
            identifier.Storage = storage2;
            identifier.Account = account2;
            identifier.Space = space2;
            identifier.Era = era2;
            identifier.Period = period2;
            identifier.Moment = moment2;

            // Assert.
            Assert.AreEqual(storage2, identifier.Storage, "The identifier.Storage property has not changed");
            Assert.AreEqual(account2, identifier.Account, "The identifier.Account property has not changed");
            Assert.AreEqual(space2, identifier.Space, "The identifier.Space property has not changed");
            Assert.AreEqual(era2, identifier.Era, "The identifier.Era property has not changed");
            Assert.AreEqual(period2, identifier.Period, "The identifier.Period property has not changed");
            Assert.AreEqual(moment2, identifier.Moment, "The identifier.Moment property has not changed");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Identifier_Based_On_Storage_Account_Space_Are_Equal()
        {
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            var first = Identifier.NewIdentifier(storage, account, space);
            var second = Identifier.NewIdentifier(storage, account, space);

            Assert.AreEqual(first, second, "The two identical root identifiers do not match");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Identifier_Based_On_Storage_Era_Is_Not_Equal()
        {
            // Arrange.
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            const ulong era1 = 1;
            const ulong period = 2;
            const ulong moment = 3;
            const ulong era2 = 4;
            var spaceIdentifier = Identifier.NewIdentifier(storage, account, space);
            var first = Identifier.NewIdentifier(spaceIdentifier, era1, period, moment);
            var second = Identifier.NewIdentifier(spaceIdentifier, era2, period, moment);

            // Act
            var result = first.Equals(second);

            // Assert.
            Assert.AreNotEqual(result, "The two identifiers should not be equal");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Identifier_Based_On_Storage_Account_Space_Period_Moment_Are_Equal()
        {
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            const ulong era = 0;
            const ulong period = 2;
            const ulong moment = 3;
            var first = Identifier.Create(storage, account, space, era, period, moment);
            var second = Identifier.Create(storage, account, space, era, period, moment);

            Assert.AreEqual(first, second, "The two identical identifiers do not match");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Identifier_Based_On_Storage_Account_Space_Period_Compare_As_Equal()
        {
            // Arrange.
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            var first = Identifier.NewIdentifier(storage, account, space);
            var second = Identifier.NewIdentifier(storage, account, space);

            // Act.
            var result = first == second;

            // Assert.
            Assert.IsTrue(result, "The two identical root identifiers do not match");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Identifier_Comparison_With_Right_Null()
        {
            // Arrange.
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            var first = Identifier.NewIdentifier(storage, account, space);
            var second = (object)null;

            // Act.
            var result = first.Equals(second);

            // Assert.
            Assert.IsFalse(result, "A identifier should not match with null");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Identifier_Comparison_With_Self()
        {
            // Arrange.
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            var first = Identifier.NewIdentifier(storage, account, space);
            var second = first as object;

            // Act.
            var result = first.Equals(second);

            // Assert.
            Assert.IsTrue(result, "A identifier should also match with itselve wrapped as object.");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Identifier_Based_On_Storage_Account_Space_Period_Moment_Compare_As_Equal()
        {
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            const ulong era = 0;
            const ulong period = 2;
            const ulong moment = 3;
            var first = Identifier.Create(storage, account, space, era, period, moment);
            var second = Identifier.Create(storage, account, space, era, period, moment);

            Assert.IsTrue(first == second, "The two identical identifiers do not match");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Identifier_Based_On_Storage_Account_Space_Period_Moment_Do_Not_Compare_As_Equal()
        {
            var firstStorage = Guid.NewGuid();
            var secondStorage = Guid.NewGuid();
            var firstAccount = Guid.NewGuid();
            var secondAccount = Guid.NewGuid();
            var firstSpace = Guid.NewGuid();
            var secondSpace = Guid.NewGuid();

            var first = Identifier.Create(firstStorage, firstAccount, firstSpace,0 , 2, 3);
            var second = Identifier.Create(secondStorage, firstAccount, firstSpace, 0, 2, 3);
            Assert.IsTrue(first != second, "The two identical identifiers do not match");
            Assert.IsFalse(first == second, "The two identical identifiers do not match");

            first = Identifier.Create(firstStorage, firstAccount, firstSpace, 0, 2, 3);
            second = Identifier.Create(firstStorage, secondAccount, firstSpace, 0, 2, 3);
            Assert.IsTrue(first != second, "The two identical identifiers do not match");
            Assert.IsFalse(first == second, "The two identical identifiers do not match");

            first = Identifier.Create(firstStorage, firstAccount, firstSpace, 0, 2, 3);
            second = Identifier.Create(firstStorage, firstAccount, firstSpace, 0, 4, 3);
            Assert.IsTrue(first != second, "The two identical identifiers do not match");
            Assert.IsFalse(first == second, "The two identical identifiers do not match");

            first = Identifier.Create(firstStorage, firstAccount, firstSpace, 0, 2, 3);
            second = Identifier.Create(firstStorage, firstAccount, firstSpace, 0, 2, 4);
            Assert.IsTrue(first != second, "The two identical identifiers do not match");
            Assert.IsFalse(first == second, "The two identical identifiers do not match");

            first = Identifier.Create(firstStorage, firstAccount, firstSpace, 0, 2, 3);
            second = Identifier.Create(secondStorage, firstAccount, secondSpace, 0, 2, 3);
            Assert.IsTrue(first != second, "The two identical identifiers do not match");
            Assert.IsFalse(first == second, "The two identical identifiers do not match");
        }
    }
}
