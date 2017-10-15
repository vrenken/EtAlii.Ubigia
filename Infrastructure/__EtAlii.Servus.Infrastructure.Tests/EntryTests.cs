using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EtAlii.Servus.Model.Infrastructure.Test
{
    [TestClass]
    public class EntryTests
    {
        [TestMethod]
        public void Entry_CreateRoot_With_Root_Account()
        {
            UInt64 root = 1;
            string account = "Test";
            var entry = Entry.CreateOrigin(root, account);

            Assert.AreEqual(root, entry.Id.Root, "The entry.Id.Root property is incorrect");
            Assert.AreEqual(account, entry.Id.Account, "The entry.Id.Account property is incorrect");
            Assert.AreEqual(UInt64.MinValue, entry.Id.Moment, "The entry.Id.Period property is incorrect");
            Assert.AreEqual(UInt64.MinValue, entry.Id.Period, "The entry.Id.Moment property is incorrect");
        }

        [TestMethod]
        public void Entry_Create_With_Past()
        {
            UInt64 root = 1;
            string account = "Test";
            var rootEntry = Entry.CreateOrigin(root, account);
            var entry = Entry.Create(rootEntry);

            Assert.AreEqual(root, entry.Id.Root, "The entry.Id.Root property is incorrect");
            Assert.AreEqual(account, entry.Id.Account, "The entry.Id.Account property is incorrect");
            Assert.AreEqual(UInt64.MinValue, entry.Id.Moment, "The entry.Id.Period property is incorrect");
            Assert.AreEqual(UInt64.MinValue, entry.Id.Period, "The entry.Id.Moment property is incorrect");
        }

        [TestMethod]
        public void Entry_Based_On_Root_Account_Are_Equal()
        {
            UInt64 root = 1;
            string account = "Test";
            var first = Entry.CreateOrigin(root, account);
            var second = Entry.CreateOrigin(root, account);

            Assert.AreEqual(first, second, "The two identical entries do not match");
        }

        [TestMethod]
        public void Entry_Based_On_Root_Account_Compare_As_Equal()
        {
            UInt64 root = 1;
            string account = "Test";
            var first = Entry.CreateOrigin(root, account);
            var second = Entry.CreateOrigin(root, account);

            Assert.IsTrue(first == second, "The two identical entries do not match");
        }

        [TestMethod]
        public void Entry_Based_On_Root_Account_Do_Not_Compare_As_Equal()
        {
            var first = Entry.CreateOrigin(1, "Test");
            var second = Entry.CreateOrigin(2, "Test");
            Assert.IsFalse(first == second, "The two identical entries do not match");
            Assert.IsTrue(first != second, "The two identical entries do not match");

            first = Entry.CreateOrigin(1, "Test_1");
            second = Entry.CreateOrigin(1, "Test_2");
            Assert.IsFalse(first == second, "The two identical entries do not match");
            Assert.IsTrue(first != second, "The two identical entries do not match");
        }
    }
}
