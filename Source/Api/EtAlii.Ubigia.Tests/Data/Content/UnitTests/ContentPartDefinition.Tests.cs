namespace EtAlii.Ubigia.Tests
{
    using Xunit;

    public class ContentPartDefinitionTests
    {
        [Fact]
        public void ContentPartDefinition_Create_Check_Checksum()
        {
            var contentDefinitionPart = new ContentDefinitionPart();
            Assert.Equal((ulong)0L, contentDefinitionPart.Checksum);
        }

        [Fact]
        public void ContentPartDefinition_Create_Check_Id()
        {
            var contentDefinitionPart = new ContentDefinitionPart();
            Assert.Equal((ulong)0L, contentDefinitionPart.Id);
        }

        [Fact]
        public void ContentPartDefinition_Create_Check_Size()
        {
            var contentDefinitionPart = new ContentDefinitionPart();
            Assert.Equal((ulong)0L, contentDefinitionPart.Size);
        }

        [Fact]
        public void ContentPartDefinition_Empty_Check_Size()
        {
            var contentDefinitionPart = ContentDefinitionPart.Empty;
            Assert.Equal((ulong)0L, contentDefinitionPart.Size);
        }

        [Fact]
        public void ContentPartDefinition_Empty_Check_Checksum()
        {
            var contentDefinitionPart = ContentDefinitionPart.Empty;
            Assert.Equal((ulong)0L, contentDefinitionPart.Checksum);
        }

        [Fact]
        public void ContentPartDefinition_Empty_Check_Id()
        {
            var contentDefinitionPart = ContentDefinitionPart.Empty;
            Assert.Equal((ulong)0L, contentDefinitionPart.Id);
        }
    }
}
