namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api.Data.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ContentType_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentType_Check_Image_Id()
        {
            // Act.
            var contentType = @"Image";

            // Arrange.

            // Assert.
            Assert.AreEqual(contentType, ContentType.Image.Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentType_Check_Image_Jpeg_Id()
        {
            Assert.AreEqual(@"Image\Jpeg", ContentType.Image.Jpeg.Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentType_Check_Image_Gif_Id()
        {
            Assert.AreEqual(@"Image\Gif", ContentType.Image.Gif.Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentType_Check_Image_Png_Id()
        {
            Assert.AreEqual(@"Image\Png", ContentType.Image.PortableNetworkGraphics.Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentType_Check_Time_Id()
        {
            Assert.AreEqual(@"Time", ContentType.Time.Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentType_Check_Time_Year_Id()
        {
            Assert.AreEqual(@"Time\Year", ContentType.Time.Year.Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentType_Check_Time_Month_Id()
        {
            Assert.AreEqual(@"Time\Month", ContentType.Time.Month.Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentType_Check_Time_Day_Id()
        {
            Assert.AreEqual(@"Time\Day", ContentType.Time.Day.Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentType_Check_Time_Hour_Id()
        {
            Assert.AreEqual(@"Time\Hour", ContentType.Time.Hour.Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentType_Check_Time_Minute_Id()
        {
            Assert.AreEqual(@"Time\Minute", ContentType.Time.Minute.Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentType_Check_Structure_Id()
        {
            Assert.AreEqual(@"Structure", ContentType.Structure.Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentType_Check_Structure_Hierarchy_Id()
        {
            Assert.AreEqual(@"Structure\Hierarchy", ContentType.Structure.Hierarchy.Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentType_Check_Structure_Hierarchy_ToString()
        {
            Assert.AreEqual(@"Structure\Hierarchy", ContentType.Structure.Hierarchy.ToString());
        }
    }
}
