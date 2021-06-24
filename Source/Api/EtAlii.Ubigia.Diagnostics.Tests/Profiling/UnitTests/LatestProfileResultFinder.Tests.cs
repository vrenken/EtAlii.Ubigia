// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Diagnostics.Tests
{
    using EtAlii.Ubigia.Diagnostics.Profiling;
    using Xunit;

    public class LatestProfileResultFinderTests
    {
        [Fact]
        public void LatestProfileResultFinder_Create()
        {
            // Arrange.

            // Act.
            var latestProfileResultFinder = new LatestProfileResultFinder();

            // Assert.
            Assert.NotNull(latestProfileResultFinder);
        }
        [Fact]
        public void LatestProfileResultFinder_Find()
        {
            // Arrange.
            var latestProfileResultFinder = new LatestProfileResultFinder();

            var rootResult = new ProfilingResult(null, "Test", ProfilingLayer.Functional, "Test1");
            rootResult.Start();

            var result1 = new ProfilingResult(rootResult, "Test", ProfilingLayer.Logical, "Test1");
            var result3 = new ProfilingResult(rootResult, "Test", ProfilingLayer.Logical, "Test3");
            var result5 = new ProfilingResult(rootResult, "Test", ProfilingLayer.Logical, "Test5");
            var result4 = new ProfilingResult(rootResult, "Test", ProfilingLayer.Logical, "Test4");
            var result2 = new ProfilingResult(rootResult, "Test", ProfilingLayer.Transport, "Test2");

            rootResult.Stop();
            var profilingResults = new[] { rootResult };

            // Act.
            var latestResult = latestProfileResultFinder.Find(profilingResults, ProfilingLayer.Logical);

            // Assert.
            Assert.NotNull(latestResult);
            Assert.NotNull(result1);
            Assert.NotNull(result3);
            Assert.NotNull(result5);
            Assert.NotNull(result4);
            Assert.Equal(result2, latestResult); // Not completely sure why this test returns result2.
        }
    }
}
