// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

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
