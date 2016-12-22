namespace EtAlii.Servus.Api.Diagnostics.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Diagnostics.Profiling;
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
