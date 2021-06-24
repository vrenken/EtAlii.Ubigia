// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Tests
{
    using EtAlii.xTechnology.Structure;
    using Xunit;

    public class SelectorTests
    {
        [Fact]
        public void Selector_Select_1()
        {
            // Arrange.
            var selector = new Selector<string, int>()
                .Register(s => s == "BaadFood", 14)
                .Register(s => s == "baadfood", 15);

            // Act.
            var result = selector.Select("BaadFood");

            // Assert.
            Assert.Equal(14, result);
        }

        [Fact]
        public void Selector_Select_2()
        {
            // Arrange.
            var selector = new Selector<string, int>()
                .Register(s => s == "baadfood", 15)
                .Register(s => s == "BaadFood", 14);

            // Act.
            var result = selector.Select("BaadFood");

            // Assert.
            Assert.Equal(14, result);
        }

        [Fact]
        public void Selector_Select_3()
        {
            // Arrange.
            var selector = new Selector<string, int>()
                .Register(s => s == "baadfood", 0)
                .Register(s => s == "BaadFood", 14);

            // Act.
            var result = selector.Select("BaadFood");

            // Assert.
            Assert.Equal(14, result);
        }

        [Fact]
        public void Selector_TrySelect_1()
        {
            // Arrange.
            var selector = new Selector<string, int>()
                .Register(s => s == "BaadFood", 14)
                .Register(s => s == "baadfood", 15);

            // Act.
            var result = selector.TrySelect("BaadFood");

            // Assert.
            Assert.Equal(14, result);
        }

        [Fact]
        public void Selector_TrySelect_2()
        {
            // Arrange.
            var selector = new Selector<string, int>()
                .Register(s => s == "baadfood", 15)
                .Register(s => s == "BaadFood", 14);

            // Act.
            var result = selector.TrySelect("BaadFood");

            // Assert.
            Assert.Equal(14, result);
        }

        [Fact]
        public void Selector_TrySelect_3()
        {
            // Arrange.
            var selector = new Selector<string, int>()
                .Register(s => s == "baadfood", 0)
                .Register(s => s == "BaadFood", 14);

            // Act.
            var result = selector.TrySelect("BaadFood");

            // Assert.
            Assert.Equal(14, result);
        }

        [Fact]
        public void Selector_TrySelect_4()
        {
            // Arrange.
            var selector = new Selector<string, int>()
                .Register(s => s == "baadfood", 0)
                .Register(s => s == "BaadFood", 14);

            // Act.
            var result = selector.TrySelect("BaadFood+");

            // Assert.
            Assert.Equal(0, result);
        }


        [Fact]
        public void Selector_TrySelect_5()
        {
            // Arrange.
            var selector = new Selector<string, int>()
                .Register(s => s == "baadfood", 0)
                .Register(s => s == "BaadFood", 14);

            // Act.
            var result = selector.TrySelect("BaadFood+");

            // Assert.
            Assert.Equal(0, result);
        }
    }
}
