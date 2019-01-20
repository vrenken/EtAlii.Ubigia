namespace EtAlii.Ubigia.Provisioning.Tests
{
    using System;
    using Xunit;

    public class ProviderContextTests
    {
        [Fact(Skip = "Blocking the build server")]
        public void ProviderContext_Create()
        {
            // Arrange.

            // Act.
            var act = new Action(() =>
            {
                var context = new ProviderContext(null, null, null);
            });

            // Assert.
            Assert.Throws<InvalidOperationException>(act);
        }
    }
}
