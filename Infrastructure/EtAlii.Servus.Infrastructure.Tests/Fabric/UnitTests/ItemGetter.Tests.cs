namespace EtAlii.Servus.Infrastructure.Tests.UnitTests
{
    using Xunit;

    
    public sealed class ItemGetter_Tests
    {
        //[Fact]
        //public void ItemGetter_Get()
        //{
        //    var storage = new StubIItemStorage();

        //    var itemGetter = new ItemGetter(storage);

        //    var firstId = Guid.NewGuid();
        //    var secondId = Guid.NewGuid();
        //    var thirdId = Guid.NewGuid();

        //    var items = new IIdentifiable[] 
        //    { 
        //        new Space { Id = firstId },
        //        new Space { Id = secondId },
        //        new Space { Id = thirdId },
        //    };

        //    var item = itemGetter.Get(items, secondId);

        //    Assert.Equal(secondId, item.Id);
        //}

        //[Fact]
        //public void ItemGetter_Get_No_ID()
        //{
        //    // Arrange.
        //    var storage = new StubIItemStorage();
        //    var itemGetter = new ItemGetter(storage);
        //    var items = new IIdentifiable[] { new Space() };

        //    // Act.
        //    var act = new Action(() =>
        //    {
        //        itemGetter.Get(items, Guid.Empty);
        //    });

        //    // Assert.
        //    ExceptionAssert.Throws<ArgumentException>(act);
        //}
    }
}
