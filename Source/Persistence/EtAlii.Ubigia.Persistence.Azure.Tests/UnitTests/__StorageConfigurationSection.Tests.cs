// // Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia
//
// namespace EtAlii.Ubigia.Persistence.Tests
// {
//     using EtAlii.Ubigia.Persistence.Azure;
//     using Xunit;
//     using EtAlii.Ubigia.Tests;
//
//     [CorrelateUnitTests]
//     public class StorageConfigurationSectionTests
//     {
//         [Fact, Trait("Category", TestAssembly.Category)]
//         public void StorageConfigurationSection_Create()
//         {
//             // Arrange.
//
//             // Act.
//             var storageConfigurationSection = new StorageConfigurationSection();
//
//             // Assert.
//             Assert.NotNull(storageConfigurationSection);
//         }
//
//         [Fact, Trait("Category", TestAssembly.Category)]
//         public void StorageConfigurationSection_ToStorageConfiguration()
//         {
//             // Arrange.
//             var storageConfigurationSection = new StorageConfigurationSection {Name = "Test"};
//
//             // Act.
//             var storageConfiguration = storageConfigurationSection.ToStorageConfiguration();
//
//             // Assert.
//             Assert.NotNull(storageConfiguration);
//             Assert.Equal("Test", storageConfiguration.Name);
//         }
//     }
// }
