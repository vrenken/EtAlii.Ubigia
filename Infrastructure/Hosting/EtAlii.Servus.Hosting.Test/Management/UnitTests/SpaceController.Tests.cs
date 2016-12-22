namespace EtAlii.Servus.Infrastructure.Hosting.UnitTests
{
    using EtAlii.Servus.Infrastructure.WebApi;
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
