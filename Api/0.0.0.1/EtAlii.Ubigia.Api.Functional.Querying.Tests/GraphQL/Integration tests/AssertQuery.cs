namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Threading.Tasks;
    using GraphQL.Http;
    using Newtonsoft.Json.Linq;
    using Xunit;

    public static class AssertQuery
    {
        public static async Task ResultsAreEqual(IDocumentWriter documentWriter, string expected, QueryProcessingResult actual)
        {
            var expectedResult = new QueryProcessingResult(JObject.Parse(expected), String.Empty);
            await ResultsAreEqual(documentWriter, expectedResult, actual);
        }
        
        public static async Task ResultsAreEqual(IDocumentWriter documentWriter, QueryProcessingResult expected, QueryProcessingResult actual)
        {
            var expectedString = await documentWriter.WriteToStringAsync(QueryProcessingResult.ToGraphQlExecutionResult(expected));
            var actualString = await documentWriter.WriteToStringAsync(QueryProcessingResult.ToGraphQlExecutionResult(actual));
            Assert.Equal(expectedString, actualString);
        }
        
        public static async Task ResultsAreNotEqual(IDocumentWriter documentWriter, string expected, QueryProcessingResult actual)
        {
            var expectedResult = new QueryProcessingResult(JObject.Parse(expected), String.Empty);
            await ResultsAreNotEqual(documentWriter, expectedResult, actual);
        }
        public static async Task ResultsAreNotEqual(IDocumentWriter documentWriter, QueryProcessingResult expected, QueryProcessingResult actual)
        {
//            var expectedDocument = JObject.Parse(expected)
            var expectedString = await documentWriter.WriteToStringAsync(QueryProcessingResult.ToGraphQlExecutionResult(expected));
            var actualString = await documentWriter.WriteToStringAsync(QueryProcessingResult.ToGraphQlExecutionResult(actual));
            Assert.NotEqual(expectedString, actualString);
        }
    }
}