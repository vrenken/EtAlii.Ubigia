namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal class NewLineParser : INewLineParser
    {
        public LpsChain Required { get; }

        public LpsChain Optional { get; }

        public LpsParser OptionalMultiple { get; }

        public NewLineParser(IWhitespaceParser whitespaceParser)
        {
            Required = whitespaceParser.Optional + Lp.OneOrMore(c => c == '\n') + whitespaceParser.Optional;
            Optional = whitespaceParser.Optional + Lp.ZeroOrMore(c => c == '\n') + whitespaceParser.Optional;

            OptionalMultiple = Lp.ZeroOrMore(c => c == ' ' || c == '\n' || c == '\r' || c == '\t');
            //_optionalMultiple = (Lp.ZeroOrMore(c => c == ' ') + Lp.ZeroOrMore(c => c == '\n') + Lp.ZeroOrMore(c => c == ' ')).ZeroOrMore().Maybe()
        }
    }
}
