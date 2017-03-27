namespace EtAlii.Ubigia.Provisioning.UnitTests
{
    using Xunit;

    public class ProviderContext_Tests
    {
        [Fact]
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
