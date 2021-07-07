// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Local
{
    using System;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Hosting.Diagnostics;
    using Xunit;

    public class InstanceCreatorTests
    {
        [Fact]
        public void InstanceCreator_CreateWithDecorator()
        {
            // Arrange.
            var diagnostics = DiagnosticsConfiguration.Default; // We need this to trigger the logging.
            var decoree = new InstanceCreator();
            var instanceCreator = new LoggingInstanceCreator(decoree);

            // Act.
            var act = new Action(() => instanceCreator.TryCreate<object>(null, null, null, "Test", out var _));

            // Assert.
            Assert.Throws<NullReferenceException>(act);
            Assert.NotNull(diagnostics);
            Assert.NotNull(instanceCreator);
        }
    }
}
