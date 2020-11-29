namespace EtAlii.Ubigia.Diagnostics.Tests
{
    using System;
    using EtAlii.Ubigia.Diagnostics.Profiling;
    using Xunit;

    public class ProfilerComposerTests
    {

        [Fact]
        public void ProfilerComposer_Create()
        {
            // Arrange.
            
            // Act.
            var profileComposer = new ProfileComposer(Array.Empty<IProfiler>());
            
            // Assert.
            Assert.NotNull(profileComposer);
        }

    }
}
