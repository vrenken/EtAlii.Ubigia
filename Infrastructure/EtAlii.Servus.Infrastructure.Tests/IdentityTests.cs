using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EtAlii.Servus.Model.Infrastructure.Test
{
    [TestClass]
    public class IdentifierTests
    {
        [TestMethod]
        public void Identifier_Empty_DefaultValues()
        {
            Assert.AreEqual(UInt64.MinValue, Identifier.Empty.Root, "The Identifier.Empty.Root property is incorrect");
            Assert.AreEqual(String.Empty, Identifier.Empty.Account, "The Identifier.Empty.Account property is incorrect");
            Assert.AreEqual(UInt64.MinValue, Identifier.Empty.Period, "The Identifier.Empty.Period property is incorrect");
            Assert.AreEqual(UInt64.MinValue, Identifier.Empty.Moment, "The Identifier.Empty.Moment property is incorrect");
        }

        [TestMethod]
        public void Identifier_Create_With_Root_Account()
        {
            UInt64 root = 1;
            string account = "Test";
            var identifier = Identifier.Create(root, account);

            Assert.AreEqual(root, identifier.Root, "The identifier.Root property is incorrect");
            Assert.AreEqual(account, identifier.Account, "The identifier.Account property is incorrect");
        }

        [TestMethod]
        public void Identifier_Create_With_Root_Account_Period_Moment()
        {
            UInt64 root = 1;
            string account = "Test";
            UInt64 period = 2;
            UInt64 moment = 3;
            var identifier = Identifier.Create(root, account, period, moment);

            Assert.AreEqual(root, identifier.Root, "The identifier.Root property is incorrect");
            Assert.AreEqual(period, identifier.Period, "The identifier.Period property is incorrect");
            Assert.AreEqual(moment, identifier.Moment, "The identifier.Moment property is incorrect");
        }

        [TestMethod]
        public void Identifier_Based_On_Root_Account_Are_Equal()
        {
            UInt64 root = 1;
            string account = "Test";
            var first = Identifier.Create(root, account);
            var second = Identifier.Create(root, account);

            Assert.AreEqual(first, second, "The two identical identifiers do not match");
        }

        [TestMethod]
        public void Identifier_Based_On_Root_Account_Period_Moment_Are_Equal()
        {
            UInt64 root = 1;
            string account = "Test";
            UInt64 period = 2;
            UInt64 moment = 3;
            var first = Identifier.Create(root, account, period, moment);
            var second = Identifier.Create(root, account, period, moment);

            Assert.AreEqual(first, second, "The two identical identifiers do not match");
        }

        [TestMethod]
        public void Identifier_Based_On_Root_Account_Compare_As_Equal()
        {
            UInt64 root = 1;
            string account = "Test";
            var first = Identifier.Create(root, account);
            var second = Identifier.Create(root, account);

            Assert.IsTrue(first == second, "The two identical identifiers do not match");
        }

        [TestMethod]
        public void Identifier_Based_On_Root_Account_Period_Moment_Compare_As_Equal()
        {
            UInt64 root = 1;
            string account = "Test";
            UInt64 period = 2;
            UInt64 moment = 3;
            var first = Identifier.Create(root, account, period, moment);
            var second = Identifier.Create(root, account, period, moment);

            Assert.IsTrue(first == second, "The two identical identifiers do not match");
        }

        [TestMethod]
        public void Identifier_Based_On_Root_Account_Period_Moment_Do_Not_Compare_As_Equal()
        {
            var first = Identifier.Create(1, "Test", 2, 3);
            var second = Identifier.Create(2, "Test", 2, 3);
            Assert.IsTrue(first != second, "The two identical identifiers do not match");
            Assert.IsFalse(first == second, "The two identical identifiers do not match");

            first = Identifier.Create(1, "Test_1", 2, 3);
            second = Identifier.Create(1, "Test_2", 2, 3);
            Assert.IsTrue(first != second, "The two identical identifiers do not match");
            Assert.IsFalse(first == second, "The two identical identifiers do not match");

            first = Identifier.Create(1, "Test", 2, 3);
            second = Identifier.Create(1, "Test", 4, 3);
            Assert.IsTrue(first != second, "The two identical identifiers do not match");
            Assert.IsFalse(first == second, "The two identical identifiers do not match");

            first = Identifier.Create(1, "Test", 2, 3);
            second = Identifier.Create(1, "Test", 2, 4);
            Assert.IsTrue(first != second, "The two identical identifiers do not match");
            Assert.IsFalse(first == second, "The two identical identifiers do not match");
        }
    }
}
