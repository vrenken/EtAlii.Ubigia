﻿namespace EtAlii.Ubigia.Infrastructure.Hosting.UnitTests
{
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Api.Admin;
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
