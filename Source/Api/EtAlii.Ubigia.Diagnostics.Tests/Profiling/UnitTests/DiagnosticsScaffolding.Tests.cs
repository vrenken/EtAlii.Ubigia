// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Diagnostics.Tests
{
    using EtAlii.xTechnology.MicroContainer;
    using Xunit;

    public class DiagnosticsScaffoldingTests
    {
        [Fact]
        public void DiagnosticsScaffolding_Create_With_Default_Configuration()
        {
            // Arrange.

            // Act.
            var scaffolding = new DiagnosticsScaffolding();

            // Assert.
            Assert.NotNull(scaffolding);
        }

        [Fact]
        public void DiagnosticsScaffolding_Create_With_Configuration()
        {
            // Arrange.
            // var name = "EtAlii";
            // var category = "EtAlii.Ubigia.Infrastructure";
            // var configuration = new DiagnosticsFactory().Create(true, false, true,
            //     () => new DisabledLogFactory(),
            //     () => new DisabledProfilerFactory(),
            //     (factory) => factory.Create(name, category),
            //     (factory) => factory.Create(name, category));

            // Act.
            var scaffolding = new DiagnosticsScaffolding();

            // Assert.
            Assert.NotNull(scaffolding);
        }

        [Fact]
        public void DiagnosticsScaffolding_Create_With_Null_Configuration()
        {
            // Arrange.

            // Act.
            var scaffolding = new DiagnosticsScaffolding();

            // Assert.
            Assert.NotNull(scaffolding);
        }

        [Fact]
        public void DiagnosticsScaffolding_Register()
        {
            // Arrange.
            var scaffolding = new DiagnosticsScaffolding();
            var container = new Container();

            // Act.
            scaffolding.Register(container);

            // Assert.
            Assert.NotNull(scaffolding);
        }
    }
}
