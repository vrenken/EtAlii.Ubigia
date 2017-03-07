namespace EtAlii.Ubigia.Api.Tests.UnitTests
{
    using System;
    using Xunit;

    public class ContentPartDefinition_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentPartDefinition_Create_Check_Checksum()
        {
            var contentDefinitionPart = new ContentDefinitionPart();
            Assert.Equal((UInt64)0L, contentDefinitionPart.Checksum);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentPartDefinition_Create_Check_Id()
        {
            var contentDefinitionPart = new ContentDefinitionPart();
            Assert.Equal((UInt64)0L, contentDefinitionPart.Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentPartDefinition_Create_Check_Size()
        {
            var contentDefinitionPart = new ContentDefinitionPart();
            Assert.Equal((UInt64)0L, contentDefinitionPart.Size);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentPartDefinition_Empty_Check_Size()
        {
            var contentDefinitionPart = ContentDefinitionPart.Empty;
            Assert.Equal((UInt64)0L, contentDefinitionPart.Size);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentPartDefinition_Empty_Check_Checksum()
        {
            var contentDefinitionPart = ContentDefinitionPart.Empty;
            Assert.Equal((UInt64)0L, contentDefinitionPart.Checksum);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentPartDefinition_Empty_Check_Id()
        {
            var contentDefinitionPart = ContentDefinitionPart.Empty;
            Assert.Equal((UInt64)0L, contentDefinitionPart.Id);
        }
    }
}
