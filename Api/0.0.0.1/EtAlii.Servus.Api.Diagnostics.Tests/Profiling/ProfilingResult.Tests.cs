namespace EtAlii.Servus.Api.Diagnostics.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Diagnostics.Profiling;
    using Xunit;

    public class ProfilingResult_Tests
    {

        [Fact]
        public void ProfilingResult_Create()
        {
            var result = new ProfilingResult(null, "root", ProfilingLayer.Functional, "Root");
        }

        [Fact]
        public void ProfilingResult_Create_Child()
        {
            // Arrange.
            var root = new ProfilingResult(null, "root", ProfilingLayer.Functional, "Root");

            // Act.
            var profile = new ProfilingResult(null, "ScriptParser", ProfilingLayer.Functional, "Execution");

            // Assert.
            Assert.Equal(0, root.Children.Count);
        }

        [Fact]
        public void ProfilingResult_Create_Child_And_Start_01()
        {
            // Arrange.
            var root = new ProfilingResult(null, "root", ProfilingLayer.Functional, "Root");
            var profile = new ProfilingResult(root, "ScriptParser", ProfilingLayer.Functional, "Execution");

            // Act.
            profile.Start();

            // Assert.
            Assert.Equal(1, root.Children.Count);
        }

        [Fact]
        public void ProfilingResult_Create_Child_And_Start_02()
        {
            // Arrange.
            var root = new ProfilingResult(null, "root", ProfilingLayer.Functional, "Root");
            var profile1 = new ProfilingResult(root, "ScriptParser", ProfilingLayer.Functional, "Query1 execution");
            var profile2 = new ProfilingResult(root, "ScriptParser", ProfilingLayer.Functional, "Query2 execution");

            // Act.
            profile1.Start();
            profile2.Start();

            // Assert.
            Assert.Equal(2, root.Children.Count);
        }

        [Fact]
        public void ProfilingResult_Start()
        {
            // Arrange.
            var root = new ProfilingResult(null, "root", ProfilingLayer.Functional, "Root");
            var profile = new ProfilingResult(root, "ScriptParser", ProfilingLayer.Functional, "Query execution");

            // Act.
            profile.Start();

            // Assert.
            Assert.NotNull(profile[ProfilingProperty.Started]);
            Assert.NotEqual(DateTime.MinValue, profile[ProfilingProperty.Started]);
            Assert.Null(profile[ProfilingProperty.DurationTotal]);
        }

        [Fact]
        public void ProfilingResult_Start_Stop()
        {
            // Arrange.
            var root = new ProfilingResult(null, "root", ProfilingLayer.Functional, "Root");
            var profile = new ProfilingResult(null, "ScriptParser", ProfilingLayer.Functional, "Query execution");

            // Act.
            profile.Start();
            Task.Delay(1000).Wait();
            profile.Stop();

            // Assert.
            Assert.NotNull(profile[ProfilingProperty.Started]);
            Assert.NotEqual(DateTime.MinValue, profile[ProfilingProperty.Started]);
            Assert.NotNull(profile[ProfilingProperty.Stopped]);
            Assert.NotEqual(DateTime.MinValue, profile[ProfilingProperty.Stopped]);
            Assert.NotNull(profile[ProfilingProperty.DurationTotal]);
            Assert.True((double)profile[ProfilingProperty.DurationTotal] > 500);
        }

        [Fact]
        public void ProfilingResult_Assign_Property()
        {
            // Arrange.
            var root = new ProfilingResult(null, "root", ProfilingLayer.Functional, "Root");
            var profile = new ProfilingResult(root, "ScriptParser", ProfilingLayer.Functional, "Query execution");

            // Act.
            profile["Count"] = 234;

            // Assert.
            Assert.Equal(234, profile["Count"]);
        }
    }
}
