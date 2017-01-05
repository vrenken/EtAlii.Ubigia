namespace EtAlii.Ubigia.Infrastructure.Hosting.UnitTests
{
    using EtAlii.Ubigia.Infrastructure.Transport.WebApi.Api.Admin;
    using Xunit;

    
    public sealed class SpaceController_Tests 
    {
        [Fact]
        public void SpaceController_Create()
        {
            var controller = new SpaceController(null);
        }
    }
}
