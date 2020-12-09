﻿namespace EtAlii.Ubigia.Tests
{
    using System;
    using Xunit;

    public class BlobPartHelperTests
    {

        [Fact, Trait("Category", TestAssembly.Category)]
        public void BlobPartHelper_GetName_ContentDefinitionPart()
        {
            var blob = new ContentDefinitionPart();
            var name = BlobPart.GetName(blob);
            Assert.Equal(@"ContentDefinition", name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void BlobPartHelper_GetName_ContentDefinitionPart_Generic()
        {
            var name = BlobPart.GetName<ContentDefinitionPart>();
            Assert.Equal(@"ContentDefinition", name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void BlobPartHelper_GetName_ContentPart()
        {
            var blobPart = new ContentPart();
            var name = BlobPart.GetName(blobPart);
            Assert.Equal(@"Content", name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void BlobPartHelper_SetId_ContentPart()
        {
            // Arrange.
            var id = (uint)new Random().Next(0, int.MaxValue);

            // Act.
            var blobPart = new ContentPart { Id = id };

            // Assert.
            Assert.Equal(id, blobPart.Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void BlobPartHelper_GetName_ContentPart_Generic()
        {
            var name = BlobPart.GetName<ContentPart>();
            Assert.Equal(@"Content", name);
        }
    }
}
