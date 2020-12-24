namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;
    using Xunit;
    using Antlr4.Runtime;

    public class AntlrParserTests
    {
        [Fact]
        public void GtlParser_Chat()
        {
            // Arrange.
            var text = "john says \"hello\" n michael says \"world\" \n";
            var inputStream = new AntlrInputStream(text);
            var gtlLexer = new GtlLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(gtlLexer);
            var parser = new GtlParser(commonTokenStream);

            // Act.
            var context = parser.chat();
            var visitor = new GtlVisitor();
            visitor.Visit(context);

            // Assert.
            Assert.Equal(2, visitor.Lines.Count);
        }

        [Fact]
        public void GtlParser_Line()
        {
            // Arrange.
            var text = "john says \"hello\" \n";
            var inputStream = new AntlrInputStream(text);
            var gtlLexer = new GtlLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(gtlLexer);
            var parser = new GtlParser(commonTokenStream);

            // Act.
            var context = parser.line();
            var visitor = new GtlVisitor();
            var line = (GtlLine) visitor.VisitLine(context);

            // Assert.
            Assert.NotNull(line);
            Assert.Equal("john", line.Person);
            Assert.Equal("hello", line.Text);
        }

        [Fact]
        public void GtlParser_WrongLine()
        {
            // Arrange.
            var text = "john sayan \"hello\" \n";
            var inputStream = new AntlrInputStream(text);
            var gtlLexer = new GtlLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(gtlLexer);
            var parser = new GtlParser(commonTokenStream);

            // Act.
            var context = parser.line();

            // Assert.
            Assert.IsType<GtlParser.LineContext>(context);
            Assert.Equal("john", context.name().GetText());
            Assert.Null(context.SAYS());
            Assert.Equal("johnsayan\"hello\"", context.GetText());
        }
    }
}
