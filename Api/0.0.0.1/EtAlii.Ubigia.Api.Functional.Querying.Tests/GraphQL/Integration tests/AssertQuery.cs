using System.Threading.Tasks;
using GraphQL;
using GraphQL.Http;
using Newtonsoft.Json.Linq;
using Xunit;

namespace EtAlii.Ubigia.Api.Functional.Tests
{
    public static class AssertQuery
    {
        public static async Task ResultsAreSame(IDocumentWriter documentWriter, string expected, ExecutionResult actual)
        {
            var expectedResult = new ExecutionResult { Data = JObject.Parse(expected) };
            await ResultsAreSame(documentWriter, expectedResult, actual);
        }
        
        public static async Task ResultsAreSame(IDocumentWriter documentWriter, ExecutionResult expected, ExecutionResult actual)
        {
//            var expectedDocument = JObject.Parse(expected);
            var expectedString = await documentWriter.WriteToStringAsync(expected);
            var actualString = await documentWriter.WriteToStringAsync(actual);
            Assert.Equal(expectedString, actualString);
        }
    }
}