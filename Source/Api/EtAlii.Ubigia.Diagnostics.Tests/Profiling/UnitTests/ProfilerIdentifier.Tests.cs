// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Diagnostics.Tests
{
    using EtAlii.Ubigia.Diagnostics.Profiling;
    using Xunit;

    public class ProfilerIdentifierTests
    {

        [Fact]
        public void ProfilerIdentifier_Create()
        {
            // Arrange.

            // Act.
            var result = new ProfilingAspect(ProfilingLayer.Functional, "Context");

            // Assert.
            Assert.NotNull(result);
        }

        [Fact]
        public void ProfilerIdentifier_Layer()
        {
            // Arrange.

            // Act.
            var identifier = new ProfilingAspect(ProfilingLayer.Functional, "Context");

            // Assert.
            Assert.Equal(ProfilingLayer.Functional, identifier.Layer);
        }

        [Fact]
        public void ProfilerIdentifier_Id()
        {
            // Arrange.

            // Act.
            var identifier = new ProfilingAspect(ProfilingLayer.Functional, "Context");

            // Assert.
            Assert.Equal("Context", identifier.Id);
        }

        [Fact]
        public void ProfilerIdentifier_AreEqual_01()
        {
            // Arrange.
            var first = new ProfilingAspect(ProfilingLayer.Functional, "Context");
            var second = new ProfilingAspect(ProfilingLayer.Functional, "Context");


            // Act.
            var result = first == second;

            // Assert.
            Assert.True(result);
        }

        [Fact]
        public void ProfilerIdentifier_AreEqual_02()
        {
            // Arrange.
            var first = new ProfilingAspect(ProfilingLayer.Functional, "Traversal context");
            var second = ProfilingAspects.Functional.TraversalContext;


            // Act.
            var result = first == second;

            // Assert.
            Assert.True(result);
        }

        [Fact]
        public void ProfilerIdentifier_AreEqual_03()
        {
            // Arrange.
            var first = new ProfilingAspect(ProfilingLayer.Functional, "Context");
            var second = new ProfilingAspect(ProfilingLayer.Functional, "Context");


            // Act.
            var result = first.Equals(second);

            // Assert.
            Assert.True(result);
        }

        [Fact]
        public void ProfilerIdentifier_AreNotEqual_01()
        {
            // Arrange.
            var first = new ProfilingAspect(ProfilingLayer.Functional, "Context");
            var second = new ProfilingAspect(ProfilingLayer.Functional, "Context2");


            // Act.
            var result = first != second;

            // Assert.
            Assert.True(result);
        }

        [Fact]
        public void ProfilerIdentifier_AreNotEqual_02()
        {
            // Arrange.
            var first = new ProfilingAspect(ProfilingLayer.Functional, "Context");
            var second = new ProfilingAspect(ProfilingLayer.Functional, "Context2");


            // Act.
            var result = first == second;

            // Assert.
            Assert.False(result);
        }

        [Fact]
        public void ProfilerIdentifier_AreNotEqual_03()
        {
            // Arrange.
            var first = new ProfilingAspect(ProfilingLayer.Functional, "Context");
            var second = new ProfilingAspect(ProfilingLayer.Fabric, "Context");


            // Act.
            var result = first == second;

            // Assert.
            Assert.False(result);
        }

        [Fact]
        public void ProfilerIdentifier_AreNotEqual_04()
        {
            // Arrange.
            var first = new ProfilingAspect(ProfilingLayer.Functional, "Context");
            var second = new ProfilingAspect(ProfilingLayer.Fabric, "Context");


            // Act.
            var result = first != second;

            // Assert.
            Assert.True(result);
        }
    }
}
