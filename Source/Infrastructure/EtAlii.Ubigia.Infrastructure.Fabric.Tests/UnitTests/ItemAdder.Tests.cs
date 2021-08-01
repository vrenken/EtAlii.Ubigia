// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public sealed class ItemAdderTests
    {
        [Fact]
        public void ItemAdder_Add()
        {
            var itemAdder = new ItemAdder();

            var firstId = Guid.NewGuid();
            var secondId = Guid.NewGuid();
            var thirdId = Guid.NewGuid();

            var items = new List<IIdentifiable>(new IIdentifiable[]
            {
                new Space { Id = firstId },
                new Space { Id = secondId },
                new Space { Id = thirdId },
            });

            var item = itemAdder.Add(items, new Space { Id = Guid.Empty, Name = "Test" });

            Assert.Equal("Test", item.Name);
        }

        [Fact]
        public void ItemAdder_Add_Already_Existing()
        {
            // Arrange.
            var itemAdder = new ItemAdder();
            var firstId = Guid.NewGuid();
            var secondId = Guid.NewGuid();
            var thirdId = Guid.NewGuid();
            var items = new List<IIdentifiable>(new IIdentifiable[]
            {
                new Space { Id = firstId },
                new Space { Id = secondId, Name = "Test" },
                new Space { Id = thirdId },
            });

            // Act.
            var act = new Action(() =>
            {
                itemAdder.Add(items, new Space { Id = Guid.Empty, Name = "Test" });
            });

            // Assert.
            Assert.Throws<InvalidOperationException>(act);
        }

        [Fact]
        public void ItemAdder_Add_With_ID_Assigned()
        {
            // Arrange.
            var itemAdder = new ItemAdder();
            var firstId = Guid.NewGuid();
            var secondId = Guid.NewGuid();
            var thirdId = Guid.NewGuid();
            var fourthId = Guid.NewGuid();
            var items = new List<IIdentifiable>(new IIdentifiable[]
            {
                new Space { Id = firstId },
                new Space { Id = secondId },
                new Space { Id = thirdId },
            });

            // Act.
            var act = new Action(() =>
            {
                itemAdder.Add(items, new Space { Id = fourthId, Name = "Test" });
            });

            // Assert.
            Assert.Throws<InvalidOperationException>(act);
        }

        [Fact]
        public void ItemAdder_Add_No_Item()
        {
            // Arrange.
            var itemAdder = new ItemAdder();
            var firstId = Guid.NewGuid();
            var secondId = Guid.NewGuid();
            var thirdId = Guid.NewGuid();

            var items = new List<IIdentifiable>(new IIdentifiable[]
            {
                new Space { Id = firstId },
                new Space { Id = secondId },
                new Space { Id = thirdId },
            });

            // Act.
            var act = new Action(() =>
            {
                itemAdder.Add(items, null);
            });

            // Assert.
            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void ItemAdder_Add_With_Error()
        {
            // Arrange.
            var itemAdder = new ItemAdder();
            var firstId = Guid.NewGuid();
            var secondId = Guid.NewGuid();
            var thirdId = Guid.NewGuid();

            var items = new ObservableCollection<IIdentifiable>(new IIdentifiable[]
            {
                new Space { Id = firstId },
                new Space { Id = secondId },
                new Space { Id = thirdId },
            });
            items.CollectionChanged += (_, _) => throw new ApplicationException();

            // Act.
            var act = new Action(() =>
            {
                itemAdder.Add(items, new Space { Id = Guid.Empty, Name = "Test" });
            });

            // Assert.
            Assert.Throws<ApplicationException>(act);
        }
    }
}
