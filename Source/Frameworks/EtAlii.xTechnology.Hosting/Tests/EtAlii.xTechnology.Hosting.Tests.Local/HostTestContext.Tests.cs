namespace EtAlii.xTechnology.Hosting.Tests.Local
{
    using System.Threading.Tasks;
    using Xunit;

    public class HostTestContextTests
    {
        [Theory, ClassData(typeof(ConfigurationFiles))]
        public void HostTestContext_Create(string configurationFile)
        {
            // Arrange.
         
            // Act.
            var context = new HostTestContext(configurationFile);
   
            // Assert.
            Assert.NotNull(context);
            Assert.Null(context.Host);
        }
        
        [Theory, ClassData(typeof(ConfigurationFiles))]
        public async Task HostTestContext_Start(string configurationFile)
        {
            // Arrange.
            var context = new HostTestContext(configurationFile);

            // Act.
            await context.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);
            
            // Assert.
            Assert.NotNull(context);
            Assert.NotNull(context.Host);
        }
        
        [Theory, ClassData(typeof(ConfigurationFiles))]
        public async Task HostTestContext_Start_Stop(string configurationFile)
        {
            // Arrange.
            var context = new HostTestContext(configurationFile);
            await context.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);

            // Act.
            await context.Stop().ConfigureAwait(false);
            
            // Assert.
            Assert.NotNull(context);
            Assert.Null(context.Host);
        }
    }
}
