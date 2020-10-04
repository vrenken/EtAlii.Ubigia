namespace EtAlii.Ubigia.Persistence.Tests
{
    using Xunit;

    public class DiagnosticsTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void LoggingBlobPartRetriever_Create()
        {
            // Arrange.

            // Act.
            var loggingBlobPartRetriever = new LoggingBlobPartRetriever(null);

            // Assert.
            Assert.NotNull(loggingBlobPartRetriever);
        }
    }
}