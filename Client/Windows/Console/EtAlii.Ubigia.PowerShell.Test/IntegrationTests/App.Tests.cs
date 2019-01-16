namespace EtAlii.Ubigia.PowerShell.Tests
{
    using Xunit;

    
    public class AppTests
    {
        public AppTests()
        {
            PowerShellClient.Current = new PowerShellClientFactory().Create<PowerShellClient>();
        }

        [Fact]
        public void App_Container_IsNotNull() 
        {
            Assert.NotNull(PowerShellClient.Current);
        }

        [Fact]
        public void App_Container_All_Types_Registered()
        {
            Assert.NotNull(PowerShellClient.Current.EntryResolver);
            Assert.NotNull(PowerShellClient.Current.StorageResolver);
            Assert.NotNull(PowerShellClient.Current.AccountResolver);
            Assert.NotNull(PowerShellClient.Current.SpaceResolver);
        }
    }
}
