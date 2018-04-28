namespace EtAlii.Ubigia.Api.Transport.WebApi.Tests
{
    using Xunit;

    
    public class PayloadMediaTypeFormatterTests
    {
        [Fact]
        public void PayloadMediaTypeFormatter_Create()
        {
            // Arrange.
            
            // Act.
            var formatter = new PayloadMediaTypeFormatter();
            
            // Assert.
            Assert.NotNull(formatter);
        }
    }
}
