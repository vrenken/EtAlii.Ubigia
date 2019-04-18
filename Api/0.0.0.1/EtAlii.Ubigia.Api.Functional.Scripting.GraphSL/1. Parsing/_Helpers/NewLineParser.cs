namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal class NewLineParser : INewLineParser
    {
        public LpsChain Required { get; }

        public LpsChain Optional { get; }

        public LpsParser OptionalMultiple { get; }

        public NewLineParser()
        {
            Required = Lp.ZeroOrMore(c => c == ' ') + Lp.OneOrMore(c => c == '\n') + Lp.ZeroOrMore(c => c == ' ');
            Optional = Lp.ZeroOrMore(c => c == ' ') + Lp.ZeroOrMore(c => c == '\n') + Lp.ZeroOrMore(c => c == ' ');

            OptionalMultiple = Lp.ZeroOrMore(c => c == ' ' || c == '\n');
            //_optionalMultiple = (Lp.ZeroOrMore(c => c == ' ') + Lp.ZeroOrMore(c => c == '\n') + Lp.ZeroOrMore(c => c == ' ')).ZeroOrMore().Maybe()
        }
    }
}