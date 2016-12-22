namespace EtAlii.Servus.Api.Fabric.Tests
{
    using EtAlii.Servus.Api.Tests;
    using Xunit;

    
    public class ContentType_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentType_Check_Image_Id()
        {
            // Act.
            var contentType = @"Image";

            // Arrange.

            // Assert.
            Assert.Equal(contentType, ContentType.Image.Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentType_Check_Image_Jpeg_Id()
        {
            Assert.Equal(@"Image\Jpeg", ContentType.Image.Jpeg.Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentType_Check_Image_Gif_Id()
        {
            Assert.Equal(@"Image\Gif", ContentType.Image.Gif.Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentType_Check_Image_Png_Id()
        {
            Assert.Equal(@"Image\Png", ContentType.Image.PortableNetworkGraphics.Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentType_Check_Time_Id()
        {
            Assert.Equal(@"Time", ContentType.Time.Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentType_Check_Time_Year_Id()
        {
            Assert.Equal(@"Time\Year", ContentType.Time.Year.Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentType_Check_Time_Month_Id()
        {
            Assert.Equal(@"Time\Month", ContentType.Time.Month.Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentType_Check_Time_Day_Id()
        {
            Assert.Equal(@"Time\Day", ContentType.Time.Day.Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentType_Check_Time_Hour_Id()
        {
            Assert.Equal(@"Time\Hour", ContentType.Time.Hour.Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentType_Check_Time_Minute_Id()
        {
            Assert.Equal(@"Time\Minute", ContentType.Time.Minute.Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentType_Check_Structure_Id()
        {
            Assert.Equal(@"Structure", ContentType.Structure.Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentType_Check_Structure_Hierarchy_Id()
        {
            Assert.Equal(@"Structure\Hierarchy", ContentType.Structure.Hierarchy.Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentType_Check_Structure_Hierarchy_ToString()
        {
            Assert.Equal(@"Structure\Hierarchy", ContentType.Structure.Hierarchy.ToString());
        }
    }
}
