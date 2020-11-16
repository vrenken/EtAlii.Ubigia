namespace EtAlii.Ubigia.Api.Functional.Querying.Tests
{
    using System.Threading.Tasks;
    using GraphQL;
    using Newtonsoft.Json.Linq;
    using Xunit;

    public static class AssertQuery
    {
        public static async Task ResultsAreEqual(IDocumentWriter documentWriter, string expected, GraphQLQueryProcessingResult actual)
        {
            var expectedResult = new GraphQLQueryProcessingResult(JObject.Parse(expected), string.Empty);
            await ResultsAreEqual(documentWriter, expectedResult, actual);
        }
        
        public static async Task ResultsAreEqual(IDocumentWriter documentWriter, GraphQLQueryProcessingResult expected, GraphQLQueryProcessingResult actual)
        {
            var expectedString = await documentWriter.WriteToStringAsync(GraphQLQueryProcessingResult.ToGraphQlExecutionResult(expected));
            var actualString = await documentWriter.WriteToStringAsync(GraphQLQueryProcessingResult.ToGraphQlExecutionResult(actual));
            Assert.Equal(expectedString, actualString);
        }
        
        public static async Task ResultsAreNotEqual(IDocumentWriter documentWriter, string expected, GraphQLQueryProcessingResult actual)
        {
            var expectedResult = new GraphQLQueryProcessingResult(JObject.Parse(expected), string.Empty);
            await ResultsAreNotEqual(documentWriter, expectedResult, actual);
        }
        public static async Task ResultsAreNotEqual(IDocumentWriter documentWriter, GraphQLQueryProcessingResult expected, GraphQLQueryProcessingResult actual)
        {
//            var expectedDocument = JObject.Parse(expected)
            var expectedString = await documentWriter.WriteToStringAsync(GraphQLQueryProcessingResult.ToGraphQlExecutionResult(expected));
            var actualString = await documentWriter.WriteToStringAsync(GraphQLQueryProcessingResult.ToGraphQlExecutionResult(actual));
            Assert.NotEqual(expectedString, actualString);
        }
    }
}