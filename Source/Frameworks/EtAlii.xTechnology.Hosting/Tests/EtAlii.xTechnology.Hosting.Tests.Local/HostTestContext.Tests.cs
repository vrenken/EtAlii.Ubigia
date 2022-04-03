// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Local
{
    using System.Threading.Tasks;
    using Xunit;

    public class HostTestContextTests
    {
        [Theory, ClassData(typeof(ConfigurationFiles))]
        public async Task HostTestContext_Create(string configurationFile)
        {
            // Arrange.

            // Act.
            var context = new LocalHostTestContext(configurationFile, ConfigurationFiles.ClientSettings);

            // Assert.
            Assert.NotNull(context);
            Assert.Null(context.Host);

            // Assure.
            await context.Stop().ConfigureAwait(false);
        }

        [Theory, ClassData(typeof(ConfigurationFiles))]
        public async Task HostTestContext_Start(string configurationFile)
        {
            // Arrange.
            var context = new LocalHostTestContext(configurationFile, ConfigurationFiles.ClientSettings);

            // Act.
            await context
                .Start()
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(context);
            Assert.NotNull(context.Host);

            // Assure.
            await context
                .Stop()
                .ConfigureAwait(false);
        }

        [Theory, ClassData(typeof(ConfigurationFiles))]
        public async Task HostTestContext_Start_Stop(string configurationFile)
        {
            // Arrange.
            var context = new LocalHostTestContext(configurationFile, ConfigurationFiles.ClientSettings);
            await context
                .Start()
                .ConfigureAwait(false);

            // Act.
            await context
                .Stop()
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(context);
            Assert.Null(context.Host);

            // Assure.
            await context.Stop().ConfigureAwait(false);
        }
    }
}
