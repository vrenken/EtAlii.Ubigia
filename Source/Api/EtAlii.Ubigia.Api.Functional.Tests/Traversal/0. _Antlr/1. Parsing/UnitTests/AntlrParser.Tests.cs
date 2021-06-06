namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using System.Linq;
    using Xunit;
    using Antlr4.Runtime;
    using EtAlii.Ubigia.Api.Functional.Antlr;

    public class AntlrParserTests
    {
        [Fact]
        public void UbigiaParser_Single_Sequence_Part()
        {
            // Arrange.
            var text = "/Documents/Test/Readme.txt" + Environment.NewLine; // A newline is always required - the AntlrScriptParser adds it by default.
            var inputStream = new AntlrInputStream(text);
            var gtlLexer = new UbigiaLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(gtlLexer);
            var parser = new UbigiaParser(commonTokenStream);

            // Act.
            var context = parser.script();
            var visitor = new UbigiaVisitor();
            var script = visitor.Visit(context) as Script;

            // Assert.
            Assert.NotNull(script);
            Assert.Single(script.Sequences);
            Assert.Equal(" <= /Documents/Test/Readme.txt", script.Sequences.ToArray()[0].ToString());
        }


        [Fact]
        public void UbigiaParser_Single_Sequence_Part_PostFixed_With_Newline()
        {
            // Arrange.
            var text = "/Documents/Test/Readme.txt\n";
            var inputStream = new AntlrInputStream(text);
            var gtlLexer = new UbigiaLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(gtlLexer);
            var parser = new UbigiaParser(commonTokenStream);

            // Act.
            var context = parser.script();
            var visitor = new UbigiaVisitor();
            var script = visitor.Visit(context) as Script;

            // Assert.
            Assert.NotNull(script);
            Assert.Single(script.Sequences);
            Assert.Equal(" <= /Documents/Test/Readme.txt", script.Sequences.ToArray()[0].ToString());
        }

        [Fact]
        public void UbigiaParser_Single_Sequence_Part_PostFixed_With_Newline_And_Space()
        {
            // Arrange.
            var text = "/Documents/Test/Readme.txt\n ";
            var inputStream = new AntlrInputStream(text);
            var gtlLexer = new UbigiaLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(gtlLexer);
            var parser = new UbigiaParser(commonTokenStream);

            // Act.
            var context = parser.script();
            var visitor = new UbigiaVisitor();
            var script = visitor.Visit(context) as Script;

            // Assert.
            Assert.NotNull(script);
            Assert.Single(script.Sequences);
            Assert.Equal(" <= /Documents/Test/Readme.txt", script.Sequences.ToArray()[0].ToString());
        }

        [Fact]
        public void UbigiaParser_Single_Sequence_Part_Prefixed_With_Newline()
        {
            // Arrange.
            var text = "\n/Documents/Test/Readme.txt" + Environment.NewLine;
            var inputStream = new AntlrInputStream(text);
            var gtlLexer = new UbigiaLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(gtlLexer);
            var parser = new UbigiaParser(commonTokenStream);

            // Act.
            var context = parser.script();
            var visitor = new UbigiaVisitor();
            var script = visitor.Visit(context) as Script;

            // Assert.
            Assert.NotNull(script);
            Assert.Single(script.Sequences);
            Assert.Equal(" <= /Documents/Test/Readme.txt", script.Sequences.ToArray()[0].ToString());
        }

        [Fact]
        public void UbigiaParser_Single_Sequence_Part_Prefixed_With_Newlines_And_Spaces()
        {
            // Arrange.
            var text = " \n/Documents/Test/Readme.txt" + Environment.NewLine;
            var inputStream = new AntlrInputStream(text);
            var gtlLexer = new UbigiaLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(gtlLexer);
            var parser = new UbigiaParser(commonTokenStream);

            // Act.
            var context = parser.script();
            var visitor = new UbigiaVisitor();
            var script = visitor.Visit(context) as Script;

            // Assert.
            Assert.NotNull(script);
            Assert.Single(script.Sequences);
            Assert.Equal(" <= /Documents/Test/Readme.txt", script.Sequences.ToArray()[0].ToString());
        }

        [Fact]
        public void UbigiaParser_Line()
        {
            // Arrange.
            var text = "-- This is a comment";
            var inputStream = new AntlrInputStream(text);
            var gtlLexer = new UbigiaLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(gtlLexer);
            var parser = new UbigiaParser(commonTokenStream);

            // Act.
            var context = parser.script();
            var visitor = new UbigiaVisitor();
            var script = visitor.Visit(context) as Script;

            // Assert.
            Assert.NotNull(script);
            Assert.Single(script.Sequences);
            Assert.Equal("-- This is a comment", script.Sequences.ToArray()[0].ToString());
        }

        [Fact]
        public void UbigiaParser_Two_Sequence_Parts()
        {
            // Arrange.
            var text = "-- This is a comment\n/Documents/Test/Readme.txt" + Environment.NewLine;
            var inputStream = new AntlrInputStream(text);
            var gtlLexer = new UbigiaLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(gtlLexer);
            var parser = new UbigiaParser(commonTokenStream);

            // Act.
            var context = parser.script();
            var visitor = new UbigiaVisitor();
            var script = visitor.Visit(context) as Script;


            // Assert.
            Assert.NotNull(script);
            Assert.Equal(2, script.Sequences.Count());
            Assert.Equal("-- This is a comment", script.Sequences.ToArray()[0].ToString());
            Assert.Equal(" <= /Documents/Test/Readme.txt", script.Sequences.ToArray()[1].ToString());
        }

        [Fact]
        public void UbigiaParser_Two_Sequence_Parts_PostFixed_With_Newline()
        {
            // Arrange.
            var text = "-- This is a comment\n/Documents/Test/Readme.txt\n";
            var inputStream = new AntlrInputStream(text);
            var gtlLexer = new UbigiaLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(gtlLexer);
            var parser = new UbigiaParser(commonTokenStream);

            // Act.
            var context = parser.script();
            var visitor = new UbigiaVisitor();
            var script = visitor.Visit(context) as Script;


            // Assert.
            Assert.NotNull(script);
            Assert.Equal(2, script.Sequences.Count());
            Assert.Equal("-- This is a comment", script.Sequences.ToArray()[0].ToString());
            Assert.Equal(" <= /Documents/Test/Readme.txt", script.Sequences.ToArray()[1].ToString());
        }

        [Fact]
        public void UbigiaParser_Two_Sequence_Parts_PostFixed_With_Newline_And_Space()
        {
            // Arrange.
            var text = "-- This is a comment\n/Documents/Test/Readme.txt\n ";
            var inputStream = new AntlrInputStream(text);
            var gtlLexer = new UbigiaLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(gtlLexer);
            var parser = new UbigiaParser(commonTokenStream);

            // Act.
            var context = parser.script();
            var visitor = new UbigiaVisitor();
            var script = visitor.Visit(context) as Script;


            // Assert.
            Assert.NotNull(script);
            Assert.Equal(2, script.Sequences.Count());
            Assert.Equal("-- This is a comment", script.Sequences.ToArray()[0].ToString());
            Assert.Equal(" <= /Documents/Test/Readme.txt", script.Sequences.ToArray()[1].ToString());
        }

        [Fact]
        public void UbigiaParser_Two_Sequence_Parts_Prefixed_With_Newline()
        {
            // Arrange.
            var text = "\n-- This is a comment\n/Documents/Test/Readme.txt" + Environment.NewLine;
            var inputStream = new AntlrInputStream(text);
            var gtlLexer = new UbigiaLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(gtlLexer);
            var parser = new UbigiaParser(commonTokenStream);

            // Act.
            var context = parser.script();
            var visitor = new UbigiaVisitor();
            var script = visitor.Visit(context) as Script;


            // Assert.
            Assert.NotNull(script);
            Assert.Equal(2, script.Sequences.Count());
            Assert.Equal("-- This is a comment", script.Sequences.ToArray()[0].ToString());
            Assert.Equal(" <= /Documents/Test/Readme.txt", script.Sequences.ToArray()[1].ToString());
        }

        [Fact]
        public void UbigiaParser_Two_Sequence_Parts_Prefixed_With_Newline_And_Space()
        {
            // Arrange.
            var text = " \n-- This is a comment\n/Documents/Test/Readme.txt" + Environment.NewLine;
            var inputStream = new AntlrInputStream(text);
            var gtlLexer = new UbigiaLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(gtlLexer);
            var parser = new UbigiaParser(commonTokenStream);

            // Act.
            var context = parser.script();
            var visitor = new UbigiaVisitor();
            var script = visitor.Visit(context) as Script;


            // Assert.
            Assert.NotNull(script);
            Assert.Equal(2, script.Sequences.Count());
            Assert.Equal("-- This is a comment", script.Sequences.ToArray()[0].ToString());
            Assert.Equal(" <= /Documents/Test/Readme.txt", script.Sequences.ToArray()[1].ToString());
        }

        [Fact]
        public void UbigiaParser_Two_Sequence_Parts_Separated_By_Two_Newlines()
        {
            // Arrange.
            var text = "-- This is a comment\n\n/Documents/Test/Readme.txt" + Environment.NewLine;
            var inputStream = new AntlrInputStream(text);
            var gtlLexer = new UbigiaLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(gtlLexer);
            var parser = new UbigiaParser(commonTokenStream);

            // Act.
            var context = parser.script();
            var visitor = new UbigiaVisitor();
            var script = visitor.Visit(context) as Script;


            // Assert.
            Assert.NotNull(script);
            Assert.Equal(2, script.Sequences.Count());
            Assert.Equal("-- This is a comment", script.Sequences.ToArray()[0].ToString());
            Assert.Equal(" <= /Documents/Test/Readme.txt", script.Sequences.ToArray()[1].ToString());
        }

        [Fact]
        public void UbigiaParser_Two_Sequence_Parts_Separated_By_Two_Newlines_And_A_Return()
        {
            // Arrange.
            var text = "-- This is a comment\n\r\n/Documents/Test/Readme.txt" + Environment.NewLine;
            var inputStream = new AntlrInputStream(text);
            var gtlLexer = new UbigiaLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(gtlLexer);
            var parser = new UbigiaParser(commonTokenStream);

            // Act.
            var context = parser.script();
            var visitor = new UbigiaVisitor();
            var script = visitor.Visit(context) as Script;


            // Assert.
            Assert.NotNull(script);
            Assert.Equal(2, script.Sequences.Count());
            Assert.Equal("-- This is a comment", script.Sequences.ToArray()[0].ToString());
            Assert.Equal(" <= /Documents/Test/Readme.txt", script.Sequences.ToArray()[1].ToString());
        }
    }
}
