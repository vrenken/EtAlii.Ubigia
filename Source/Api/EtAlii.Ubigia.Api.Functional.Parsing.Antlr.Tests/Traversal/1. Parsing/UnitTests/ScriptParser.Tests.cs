// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System.Linq;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public partial class ScriptParserTests
    {
        [Theory, ClassData(typeof(MdFileBasedTraversalScriptData))]
        public void ScriptParser_Parse_From_MdFiles(string fileName, string line, string queryText)
        {
            // Arrange.

            // Act.
            var result = _parser.Parse(queryText);
            var lines = queryText.Split('\n');

            // Assert.
            Assert.NotNull(result);
            Assert.Empty(result.Errors);
            Assert.NotNull(line);
            Assert.NotNull(fileName);

            // Let's not assert the query if we don't have one in the original script.
            var noCode = lines.All(l => l.StartsWith("--") || string.IsNullOrWhiteSpace(l));
            if (!noCode)
            {
                var script = result.Script;
                Assert.NotNull(script);
                Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());

            }
        }
    }
}
