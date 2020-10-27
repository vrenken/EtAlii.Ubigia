namespace EtAlii.Ubigia.Tests
{
    using System;
    using Xunit;

    public class BlobPartHelperTests
    {

        [Fact, Trait("Category", TestAssembly.Category)]
        public void BlobPartHelper_GetName_ContentDefinitionPart()
        {
            var blob = new ContentDefinitionPart();
            var name = BlobPartHelper.GetName(blob);
            Assert.Equal(@"ContentDefinition", name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void BlobPartHelper_GetName_ContentDefinitionPart_Generic()
        {
            var name = BlobPartHelper.GetName<ContentDefinitionPart>();
            Assert.Equal(@"ContentDefinition", name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void BlobPartHelper_GetName_ContentPart()
        {
            var blobPart = new ContentPart();
            var name = BlobPartHelper.GetName(blobPart);
            Assert.Equal(@"Content", name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void BlobPartHelper_SetId_ContentPart()
        {
            // Arrange.
            var blobPart = new ContentPart();
            var id = (uint)new Random().Next(0, int.MaxValue);

            // Act.
            BlobPartHelper.SetId(blobPart, id);

            // Assert.
            Assert.Equal(id, blobPart.Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void BlobPartHelper_GetName_ContentPart_Generic()
        {
            var name = BlobPartHelper.GetName<ContentPart>();
            Assert.Equal(@"Content", name);
        }
    }
}
