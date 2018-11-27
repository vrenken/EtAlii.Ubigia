namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System.Threading.Tasks;
    using GraphQL;
    using GraphQL.Http;
    using Newtonsoft.Json.Linq;
    using Xunit;

    public static class AssertQuery
    {
        public static async Task ResultsAreEqual(IDocumentWriter documentWriter, string expected, ExecutionResult actual)
        {
            var expectedResult = new ExecutionResult { Data = JObject.Parse(expected) };
            await ResultsAreEqual(documentWriter, expectedResult, actual);
        }
        
        public static async Task ResultsAreEqual(IDocumentWriter documentWriter, ExecutionResult expected, ExecutionResult actual)
        {
            var expectedString = await documentWriter.WriteToStringAsync(expected);
            var actualString = await documentWriter.WriteToStringAsync(actual);
            Assert.Equal(expectedString, actualString);
        }
        
        public static async Task ResultsAreNotEqual(IDocumentWriter documentWriter, string expected, ExecutionResult actual)
        {
            var expectedResult = new ExecutionResult { Data = JObject.Parse(expected) };
            await ResultsAreNotEqual(documentWriter, expectedResult, actual);
        }
        public static async Task ResultsAreNotEqual(IDocumentWriter documentWriter, ExecutionResult expected, ExecutionResult actual)
        {
//            var expectedDocument = JObject.Parse(expected);
            var expectedString = await documentWriter.WriteToStringAsync(expected);
            var actualString = await documentWriter.WriteToStringAsync(actual);
            Assert.NotEqual(expectedString, actualString);
        }
    }
}