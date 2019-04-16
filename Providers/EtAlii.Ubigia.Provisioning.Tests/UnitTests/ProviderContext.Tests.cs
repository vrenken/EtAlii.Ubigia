namespace EtAlii.Ubigia.Provisioning.Tests
{
    using Xunit;

    public class ProviderContextTests
    {
        [Fact(Skip = "Blocking the build server")]
        public void ProviderContext_Create()
        {
            // Arrange.

            // Act.
            var context = new ProviderContext(null, null, null);

            // Assert.
            Assert.NotNull(context);
        }
    }
}
