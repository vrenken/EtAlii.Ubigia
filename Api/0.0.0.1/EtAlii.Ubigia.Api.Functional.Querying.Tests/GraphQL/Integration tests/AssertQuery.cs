namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Threading.Tasks;
    using GraphQL;
    using GraphQL.Http;
    using Newtonsoft.Json.Linq;
    using Xunit;

    public static class AssertQuery
    {
        public static async Task ResultsAreEqual(IDocumentWriter documentWriter, string expected, QueryExecutionResult actual)
        {
            var expectedResult = new QueryExecutionResult(JObject.Parse(expected), String.Empty);
            await ResultsAreEqual(documentWriter, expectedResult, actual);
        }
        
        public static async Task ResultsAreEqual(IDocumentWriter documentWriter, QueryExecutionResult expected, QueryExecutionResult actual)
        {
            var expectedString = await documentWriter.WriteToStringAsync(QueryExecutionResult.ToGraphQlExecutionResult(expected));
            var actualString = await documentWriter.WriteToStringAsync(QueryExecutionResult.ToGraphQlExecutionResult(actual));
            Assert.Equal(expectedString, actualString);
        }
        
        public static async Task ResultsAreNotEqual(IDocumentWriter documentWriter, string expected, QueryExecutionResult actual)
        {
            var expectedResult = new QueryExecutionResult(JObject.Parse(expected), String.Empty);
            await ResultsAreNotEqual(documentWriter, expectedResult, actual);
        }
        public static async Task ResultsAreNotEqual(IDocumentWriter documentWriter, QueryExecutionResult expected, QueryExecutionResult actual)
        {
//            var expectedDocument = JObject.Parse(expected);
            var expectedString = await documentWriter.WriteToStringAsync(QueryExecutionResult.ToGraphQlExecutionResult(expected));
            var actualString = await documentWriter.WriteToStringAsync(QueryExecutionResult.ToGraphQlExecutionResult(actual));
            Assert.NotEqual(expectedString, actualString);
        }
    }
}