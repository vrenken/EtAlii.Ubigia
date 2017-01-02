namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api.Data.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class PartCalculator_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void PartCalculator_Create()
        {
            var partCalculator = new ContentPartCalculator();
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void PartCalculator_GetRequiredParts_For_One_GigaByte()
        {
            // Arrange.
            var partCalculator = new ContentPartCalculator();
            UInt64 onGigaByte = 1024 * 1024 * 1024;

            // Act.
            var parts = partCalculator.GetRequiredParts(onGigaByte);

            // Assert.
            Assert.AreEqual((UInt64)1024, parts);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void PartCalculator_GetRequiredParts_For_One_GigaByte_Plus_One_Byte()
        {
            // Arrange.
            var partCalculator = new ContentPartCalculator();
            UInt64 onGigaByte = 1024 * 1024 * 1024 + 1;

            // Act.
            var parts = partCalculator.GetRequiredParts(onGigaByte);

            // Assert.
            Assert.AreEqual((UInt64)1025, parts);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void PartCalculator_GetRequiredParts_For_One_GigaByte_Minus_One_Byte()
        {
            // Arrange.
            var partCalculator = new ContentPartCalculator();
            UInt64 onGigaByte = 1024 * 1024 * 1024 - 1;

            // Act.
            var parts = partCalculator.GetRequiredParts(onGigaByte);

            // Assert.
            Assert.AreEqual((UInt64)1024
                , parts);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void PartCalculator_GetRequiredParts_For_A_Half_MegaByte()
        {
            // Arrange.
            var partCalculator = new ContentPartCalculator();
            UInt64 halfMegaByte = 1024 * 512;

            // Act.
            var parts = partCalculator.GetRequiredParts(halfMegaByte);

            // Assert.
            Assert.AreEqual((UInt64)1, parts);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void PartCalculator_GetRequiredParts_For_No_Bytes()
        {
            // Arrange.
            var partCalculator = new ContentPartCalculator();
            UInt64 noBytes = 0;

            // Act.
            var parts = partCalculator.GetRequiredParts(noBytes);

            // Assert.
            Assert.AreEqual((UInt64)0, parts);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void PartCalculator_GetPart_For_First_Byte()
        {
            // Arrange.
            var partCalculator = new ContentPartCalculator();
            UInt64 onGigaByte = 1024 * 1024 * 1024;
            UInt64 firstByte = 0;

            // Act.
            var part = partCalculator.GetPart(onGigaByte, firstByte);

            // Assert.
            Assert.AreEqual((UInt64)0, part);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void PartCalculator_GetPart_For_First_Byte_In_SecondMegaByte()
        {
            // Arrange.
            var partCalculator = new ContentPartCalculator();
            UInt64 onGigaByte = 1024 * 1024 * 1024;
            UInt64 firstByte = 1024 * 1024;

            // Act.
            var part = partCalculator.GetPart(onGigaByte, firstByte);

            // Assert.
            Assert.AreEqual((UInt64)1, part);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void PartCalculator_GetPart_For_Second_Byte_In_SecondMegaByte()
        {
            // Arrange.
            var partCalculator = new ContentPartCalculator();
            UInt64 onGigaByte = 1024 * 1024 * 1024;
            UInt64 firstByte = 1024 * 1024 + 1;

            // Act.
            var part = partCalculator.GetPart(onGigaByte, firstByte);

            // Assert.
            Assert.AreEqual((UInt64)1, part);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void PartCalculator_GetPart_For_Last_Byte_In_FirstMegaByte()
        {
            // Arrange.
            var partCalculator = new ContentPartCalculator();
            UInt64 onGigaByte = 1024 * 1024 * 1024;
            UInt64 firstByte = 1024 * 1024 - 1;

            // Act.
            var part = partCalculator.GetPart(onGigaByte, firstByte);

            // Assert.
            Assert.AreEqual((UInt64)0, part);
        }
    }
}
