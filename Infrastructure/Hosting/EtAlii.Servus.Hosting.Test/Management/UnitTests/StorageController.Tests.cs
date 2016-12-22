namespace EtAlii.Servus.Infrastructure.Hosting.UnitTests
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Infrastructure.WebApi;
    using Xunit;

    
    public sealed class StorageController_Tests 
    {
        [Fact]
        public void StorageController_Create()
        {
            var controller = new StorageController(null);
        }

        //[Fact]
        //public void StorageController_Get_Local_Storage()
        //{
        //    var controller = new StorageController();
        //    var localStorage = controller.GetLocal(String.Empty);
        //}
    }
}
