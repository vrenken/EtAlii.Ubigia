// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Moppet.Lapa;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public class RegexPathSubjectPartParserTests
    {
        [Fact]
        public void RegexPathSubjectPartParser_Create()
        {
            // Arrange.

            // Act.
            var parser = new RegexPathSubjectPartParser(new NodeValidator(), new NodeFinder());

            // Assert.
            Assert.NotNull(parser);
        }

        [Fact]
        public void RegexPathSubjectPartParser_Pattern_SingleQuoted_01()
        {
            // Arrange.
            var parser = new RegexPathSubjectPartParser(new NodeValidator(), new NodeFinder());
            var pattern = "\"\\w+\"";
            var wrappedPattern = $"['{pattern}']";

            // Act.
            var result = parser.Parser.Do(wrappedPattern);

            // Assert.
            Assert.True(result.Success);
            Assert.Equal(string.Empty, result.Rest.ToString());
            Assert.Equal(wrappedPattern, result.Match.ToString());
        }

        [Fact]
        public void RegexPathSubjectPartParser_Pattern_DoubleQuoted_01()
        {
            // Arrange.
            var parser = new RegexPathSubjectPartParser(new NodeValidator(), new NodeFinder());
            var pattern = "'\\w+'";
            var wrappedPattern = $"[\"{pattern}\"]";

            // Act.
            var result = parser.Parser.Do(wrappedPattern);

            // Assert.
            Assert.True(result.Success);
            Assert.Equal(string.Empty, result.Rest.ToString());
            Assert.Equal(wrappedPattern, result.Match.ToString());
        }


        [Fact]
        public void RegexPathSubjectPartParser_Pattern_DoubleQuoted_02()
        {
            // Arrange.
            var parser = new RegexPathSubjectPartParser(new NodeValidator(), new NodeFinder());
            var pattern = @"(((?'Open'\()[^\(\)]*)+((?'Close-Open'\))[^\(\)]*)+)*(?(Open)(?!))$";
            var wrappedPattern = $"[\"{pattern}\"]";

            // Act.
            var result = parser.Parser.Do(wrappedPattern);

            // Assert.
            Assert.True(result.Success);
            Assert.Equal(string.Empty, result.Rest.ToString());
            Assert.Equal(wrappedPattern, result.Match.ToString());
        }

        [Fact]
        public void RegexPathSubjectPartParser_Pattern_SingleQuoted_02()
        {
            // Arrange.
            var parser = new RegexPathSubjectPartParser(new NodeValidator(), new NodeFinder());

            // Patterns are copied from: https://msdn.microsoft.com/en-us/library/az24scfc(v=vs.110).aspx
            var patterns = new[]
            {
                @"\a",
                @"[\b]{3,}",
                @"(\w+)\t",
                @"\r\n(\w+)",
                @"[\v]{2,}",
                @"[\f]{2,}",
                @"\r\n(\w+)",
                @"\e",
                @"\w\040\w",
                @"\w\x20\w",
                @"\cC",
                @"\w\u0020\w",
                @"\d+[\+-x\*]\d+",
                @"[ae]",
                @"[^aei]",
                @"[A-Z]",
                @"a.e",
                @"\p{Lu}
                \p{IsCyrillic}",
                @"\P{Lu}\P{IsCyrillic}",
                @"\w",
                @"\W",
                @"\w\s",
                @"\s\S",
                @"\d",
                @"\D",
                @"^\d{3}",
                @"-\d{3}$",
                @"\A\d{3}",
                @"-\d{3}\Z",
                @"\G\(\d\)",
                @"\b\w+\s\w+\b",
                @"\Bend\w*\b",
                @"(\w)\1",
                @"(?<double>\w)\k<double>",
                //@"(((?'Open'\()[^\(\)]*)+((?'Close-Open'\))[^\(\)]*)+)*(?(Open)(?!))$",   // Moved to other unit test.
                @"Write(?:Line)?",
                @"A\d{2}(?i:\w+)\b",
                @"\w+(?=\.)",
                @"\b(?!un)\w+\b",
                @"(?<=19)\d{2}\b",
                @"(?<!19)\d{2}\b",
                @"[13579](?>A+B+)",
                @"\d*\.\d",
                "\"be+\"",
                "\"rai?n\"",
                "\",\\d{3}\"",
                "\"\\d{2,}\"",
                "\"\\d{3,5}\"",
                "\\d*?\\.\\d",
                "\"be+?\"",
                "\"rai??n\"",
                "\",\\d{3}?\"",
                "\"\\d{2,}?\"",
                "\"\\d{3,5}?\"",
                @"(\w)\1",
                @"(?<char>\w)\k<char>",
                @"th(e|is|at)",
                @"(?(A)A\d{2}\b|\b\d{3}\b)",
                "(?<quoted>\")?(? (quoted).+? \"|\\S+\\s)",
            };

            foreach (var pattern in patterns)
            {
                var wrappedPattern = $"['{pattern}']";

                // Act.
                var result = new LpsParser(parser.Parser).Do(wrappedPattern);

                // Assert.
                Assert.True(result.Success);
                Assert.Equal(string.Empty, result.Rest.ToString());
                Assert.Equal(wrappedPattern, result.Match.ToString());
            }
        }
    }
}
