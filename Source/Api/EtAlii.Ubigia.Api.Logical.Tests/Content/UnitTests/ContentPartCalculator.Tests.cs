// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using Xunit;

    public class ContentPartCalculatorTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void PartCalculator_Create()
        {
            // Arrange.

            // Act.
            var partCalculator = new ContentPartCalculator();

            // Assert.
            Assert.NotNull(partCalculator);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PartCalculator_GetRequiredParts_For_One_GigaByte()
        {
            // Arrange.
            var partCalculator = new ContentPartCalculator();
            ulong oneGigaByte = 1024 * 1024 * 1024;

            // Act.
            var parts = partCalculator.GetRequiredParts(oneGigaByte);

            // Assert.
            Assert.Equal((ulong)1024, parts);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PartCalculator_GetRequiredParts_For_One_GigaByte_Plus_One_Byte()
        {
            // Arrange.
            var partCalculator = new ContentPartCalculator();
            ulong oneGigaByte = 1024 * 1024 * 1024 + 1;

            // Act.
            var parts = partCalculator.GetRequiredParts(oneGigaByte);

            // Assert.
            Assert.Equal((ulong)1025, parts);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PartCalculator_GetRequiredParts_For_One_GigaByte_Minus_One_Byte()
        {
            // Arrange.
            var partCalculator = new ContentPartCalculator();
            ulong oneGigaByte = 1024 * 1024 * 1024 - 1;

            // Act.
            var parts = partCalculator.GetRequiredParts(oneGigaByte);

            // Assert.
            Assert.Equal((ulong)1024
                , parts);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PartCalculator_GetRequiredParts_For_A_Half_MegaByte()
        {
            // Arrange.
            var partCalculator = new ContentPartCalculator();
            ulong halfMegaByte = 1024 * 512;

            // Act.
            var parts = partCalculator.GetRequiredParts(halfMegaByte);

            // Assert.
            Assert.Equal((ulong)1, parts);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PartCalculator_GetRequiredParts_For_No_Bytes()
        {
            // Arrange.
            var partCalculator = new ContentPartCalculator();
            ulong noBytes = 0;

            // Act.
            var parts = partCalculator.GetRequiredParts(noBytes);

            // Assert.
            Assert.Equal((ulong)0, parts);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PartCalculator_GetPart_For_First_Byte()
        {
            // Arrange.
            var partCalculator = new ContentPartCalculator();
            ulong oneGigaByte = 1024 * 1024 * 1024;
            ulong firstByte = 0;

            // Act.
            var part = partCalculator.GetPart(oneGigaByte, firstByte);

            // Assert.
            Assert.Equal((ulong)0, part);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PartCalculator_GetPart_For_First_Byte_In_SecondMegaByte()
        {
            // Arrange.
            var partCalculator = new ContentPartCalculator();
            ulong oneGigaByte = 1024 * 1024 * 1024;
            ulong firstByte = 1024 * 1024;

            // Act.
            var part = partCalculator.GetPart(oneGigaByte, firstByte);

            // Assert.
            Assert.Equal((ulong)1, part);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PartCalculator_GetPart_For_Second_Byte_In_SecondMegaByte()
        {
            // Arrange.
            var partCalculator = new ContentPartCalculator();
            ulong oneGigaByte = 1024 * 1024 * 1024;
            ulong firstByte = 1024 * 1024 + 1;

            // Act.
            var part = partCalculator.GetPart(oneGigaByte, firstByte);

            // Assert.
            Assert.Equal((ulong)1, part);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PartCalculator_GetPart_For_Last_Byte_In_FirstMegaByte()
        {
            // Arrange.
            var partCalculator = new ContentPartCalculator();
            ulong oneGigaByte = 1024 * 1024 * 1024;
            ulong firstByte = 1024 * 1024 - 1;

            // Act.
            var part = partCalculator.GetPart(oneGigaByte, firstByte);

            // Assert.
            Assert.Equal((ulong)0, part);
        }
    }
}
