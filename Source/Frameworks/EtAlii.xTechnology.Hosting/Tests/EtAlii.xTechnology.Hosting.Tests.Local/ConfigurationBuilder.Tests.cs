namespace EtAlii.xTechnology.Hosting.Tests.Local
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;

    using Xunit;

    public class ConfigurationBuilderTests
    {
        [Theory, ClassData(typeof(ConfigurationFiles))]
        public async Task ConfigurationBuilder_Build(string configurationFile)
        {
            // Arrange.
            var details = await new ConfigurationDetailsParser().Parse(configurationFile);

            var configurationBuilder = new ConfigurationBuilder()
                .AddConfigurationDetails(details);

            // Act.
            var configuration = configurationBuilder.Build();

            // Assert.
            Assert.NotNull(configuration);
        }
    }
}
