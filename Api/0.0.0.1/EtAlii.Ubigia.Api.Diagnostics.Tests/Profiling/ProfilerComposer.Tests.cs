namespace EtAlii.Ubigia.Api.Diagnostics.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using Xunit;

    public class ProfilerComposer_Tests
    {

        [Fact]
        public void ProfilerComposer_Create()
        {
            var profileComposer = new ProfileComposer(new IProfiler[0]);
        }

    }
}
