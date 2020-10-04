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

            var rootResult = new ProfilingResult(null, "Test", ProfilingLayer.Functional, "Test1", true);
            rootResult.Start();

            var result1 = new ProfilingResult(rootResult, "Test", ProfilingLayer.Logical, "Test1", true);
            var result3 = new ProfilingResult(rootResult, "Test", ProfilingLayer.Logical, "Test3", true);
            var result5 = new ProfilingResult(rootResult, "Test", ProfilingLayer.Logical, "Test5", true);
            var result4 = new ProfilingResult(rootResult, "Test", ProfilingLayer.Logical, "Test4", true);
            var result2 = new ProfilingResult(rootResult, "Test", ProfilingLayer.Transport, "Test2", true);

            rootResult.Stop();
            var profilingResults = new ProfilingResult[] { rootResult };

            // Act.
            var latestResult = latestProfileResultFinder.Find(profilingResults, ProfilingLayer.Logical);

            // Assert.
            Assert.NotNull(latestResult);
            Assert.Equal(result2, latestResult); // Not completely sure why this test returns result2.
        }
    }
}
