namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api.Data;
    using EtAlii.Servus.Api.Data.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class Entry_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Entry_CreateRoot_With_Storage_Account_Space()
        {
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            var entry = Entry.NewEntry(storage, account, space);

            Assert.AreEqual(storage, entry.Id.Storage, "The entry.Id.Storage property is incorrect");
            Assert.AreEqual(account, entry.Id.Account, "The entry.Id.Account property is incorrect");
            Assert.AreEqual(space, entry.Id.Space, "The entry.Id.Space property is incorrect");
            Assert.AreEqual(UInt64.MinValue, entry.Id.Period, "The entry.Id.Moment property is incorrect");
            Assert.AreEqual(UInt64.MinValue, entry.Id.Moment, "The entry.Id.Period property is incorrect");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Entry_Create_With_Previous()
        {
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            var previousId = Identifier.NewIdentifier(storage, account, space);
            var previous = Relation.NewRelation(previousId);
            var identifier = Identifier.NewIdentifier(previousId, 0, 0, 1);
            var entry = Entry.NewEntry(identifier, previous);

            Assert.AreEqual(storage, entry.Id.Storage, "The entry.Id.Storage property is incorrect");
            Assert.AreEqual(account, entry.Id.Account, "The entry.Id.Account property is incorrect");
            Assert.AreEqual(space, entry.Id.Space, "The entry.Id.Space property is incorrect");
            Assert.AreEqual(UInt64.MinValue, entry.Id.Period, "The entry.Id.Moment property is incorrect");
            Assert.AreNotEqual(UInt64.MinValue, entry.Id.Moment, "The entry.Id.Period property is incorrect");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Entry_Based_On_Storage_Account_Space_Are_Equal()
        {
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            var first = Entry.NewEntry(storage, account, space);
            var second = Entry.NewEntry(storage, account, space);

            Assert.AreEqual(first, second, "The two identical entries do not match");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Entry_Compare_Using_Equals_Operator()
        {
            // Arrange.
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space1 = Guid.NewGuid();
            var space2 = Guid.NewGuid();
            var first = Entry.NewEntry(storage, account, space1);
            var second = Entry.NewEntry(storage, account, space2);

            // Act.
            var equals = first == second;

            // Assert.
            Assert.IsFalse(equals, "Two different entries do match");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Entry_Compare_Using_Equals_Operator_With_Left_Null()
        {
            // Arrange.
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            var entry = Entry.NewEntry(storage, account, space);

            // Act.
            var equals = null == entry;

            // Assert.
            Assert.IsFalse(equals, "A entry matches with null");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Entry_Compare_Using_Equals_Operator_With_Right_Null()
        {
            // Arrange.
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            var entry = Entry.NewEntry(storage, account, space);

            // Act.
            var equals = entry == null;

            // Assert.
            Assert.IsFalse(equals, "A entry matches with null");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Entry_Compare_Using_Equals_Operator_With_Null()
        {
            // Arrange.
            var left = (Entry)null;
            var right = (Entry)null;

            // Act.
            var equals = left == right;

            // Assert.
            Assert.IsTrue(equals, "Two null entry should match");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Entry_Compare_With_Object()
        {
            // Arrange.
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space1 = Guid.NewGuid();
            var space2 = Guid.NewGuid();
            var first = Entry.NewEntry(storage, account, space1);
            var second = Entry.NewEntry(storage, account, space2);

            // Act.
            var equals = first.Equals((object)second);

            // Assert.
            Assert.IsFalse(equals, "The two identical entries don't match");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Entry_Compare_With_Self()
        {
            // Arrange.
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space1 = Guid.NewGuid();
            var space2 = Guid.NewGuid();
            var entry = Entry.NewEntry(storage, account, space1);

            // Act.
            var equals = entry.Equals((object)entry);

            // Assert.
            Assert.IsTrue(equals, "The same entry should match");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Entry_Get_Hash()
        {
            // Arrange.
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            var entry = Entry.NewEntry(storage, account, space);

            // Act.
            var hash = entry.GetHashCode();

            // Assert.
            Assert.IsNotNull(hash, "The entry has no hash");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Entry_Based_On_Storage_Account_Space_Compare_As_Equal()
        {
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            var first = Entry.NewEntry(storage, account, space);
            var second = Entry.NewEntry(storage, account, space);

            Assert.IsTrue(first == second, "The two identical entries do not match");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Entry_Based_On_Storage_Account_Space_Do_Not_Compare_As_Equal()
        {
            var firstStorage = Guid.NewGuid();
            var secondStorage = Guid.NewGuid();
            var firstAccount = Guid.NewGuid();
            var secondAccount = Guid.NewGuid();
            var firstSpace = Guid.NewGuid();
            var secondSpace = Guid.NewGuid();

            var first = Entry.NewEntry(firstStorage, firstAccount, firstSpace);
            var second = Entry.NewEntry(secondStorage, firstAccount, firstSpace);
            Assert.IsFalse(first == second, "The two identical entries do not match");
            Assert.IsTrue(first != second, "The two identical entries do not match");

            first = Entry.NewEntry(firstStorage, firstAccount, firstSpace);
            second = Entry.NewEntry(firstStorage, secondAccount, firstSpace);
            Assert.IsFalse(first == second, "The two identical entries do not match");
            Assert.IsTrue(first != second, "The two identical entries do not match");

            first = Entry.NewEntry(firstStorage, firstAccount, firstSpace);
            second = Entry.NewEntry(firstStorage, firstAccount, secondSpace);
            Assert.IsFalse(first == second, "The two identical entries do not match");
            Assert.IsTrue(first != second, "The two identical entries do not match");
        }
    }
}
