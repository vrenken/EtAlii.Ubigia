namespace EtAlii.Servus.Api.Transport.Tests
{
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Api.Transport.WebApi;
    using Xunit;

    
    public class PayloadMediaTypeFormatter_Tests
    {
        [Fact]
        public void PayloadMediaTypeFormatter_Create()
        {
            var formatter = new PayloadMediaTypeFormatter();
        }
    }
}
