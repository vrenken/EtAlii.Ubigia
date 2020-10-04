namespace EtAlii.Ubigia.Diagnostics.Tests
{
    using EtAlii.Ubigia.Diagnostics.Profiling;
    using Xunit;

    public class ProfilerComposerTests
    {

        [Fact]
        public void ProfilerComposer_Create()
        {
            // Arrange.
            
            // Act.
            var profileComposer = new ProfileComposer(new IProfiler[0]);
            
            // Assert.
            Assert.NotNull(profileComposer);
        }

    }
}
