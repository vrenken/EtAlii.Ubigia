namespace EtAlii.Ubigia.Tests
{
    using Xunit;

    public class ContentTypeTests
    {
        [Fact]
        public void ContentType_Check_Image_Id()
        {
            // Act.
            var contentType = @"Image";

            // Arrange.

            // Assert.
            Assert.Equal(contentType, ContentType.Image.Id);
        }

        [Fact]
        public void ContentType_Check_Image_Jpeg_Id()
        {
            Assert.Equal(@"Image\Jpeg", ContentType.Image.Jpeg.Id);
        }

        [Fact]
        public void ContentType_Check_Image_Gif_Id()
        {
            Assert.Equal(@"Image\Gif", ContentType.Image.Gif.Id);
        }

        [Fact]
        public void ContentType_Check_Image_Png_Id()
        {
            Assert.Equal(@"Image\Png", ContentType.Image.PortableNetworkGraphics.Id);
        }

        [Fact]
        public void ContentType_Check_Time_Id()
        {
            Assert.Equal(@"Time", ContentType.Time.Id);
        }

        [Fact]
        public void ContentType_Check_Time_Year_Id()
        {
            Assert.Equal(@"Time\Year", ContentType.Time.Year.Id);
        }

        [Fact]
        public void ContentType_Check_Time_Month_Id()
        {
            Assert.Equal(@"Time\Month", ContentType.Time.Month.Id);
        }

        [Fact]
        public void ContentType_Check_Time_Day_Id()
        {
            Assert.Equal(@"Time\Day", ContentType.Time.Day.Id);
        }

        [Fact]
        public void ContentType_Check_Time_Hour_Id()
        {
            Assert.Equal(@"Time\Hour", ContentType.Time.Hour.Id);
        }

        [Fact]
        public void ContentType_Check_Time_Minute_Id()
        {
            Assert.Equal(@"Time\Minute", ContentType.Time.Minute.Id);
        }

        [Fact]
        public void ContentType_Check_Structure_Id()
        {
            Assert.Equal(@"Structure", ContentType.Structure.Id);
        }

        [Fact]
        public void ContentType_Check_Structure_Hierarchy_Id()
        {
            Assert.Equal(@"Structure\Hierarchy", ContentType.Structure.Hierarchy.Id);
        }

        [Fact]
        public void ContentType_Check_Structure_Hierarchy_ToString()
        {
            Assert.Equal(@"Structure\Hierarchy", ContentType.Structure.Hierarchy.ToString());
        }
    }
}
