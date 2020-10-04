namespace EtAlii.Ubigia.Diagnostics.Tests
{
    using EtAlii.Ubigia.Diagnostics.Profiling;
    using Xunit;

    public class ProfilerComposerTests
    {

        [Fact]
        public void ProfilerComposer_Create()
        {
            var profileComposer = new ProfileComposer(new IProfiler[0]);
        }

    }
}
