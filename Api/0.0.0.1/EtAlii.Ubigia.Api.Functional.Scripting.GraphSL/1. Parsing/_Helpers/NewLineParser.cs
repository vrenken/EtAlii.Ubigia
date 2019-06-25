namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal class NewLineParser : INewLineParser
    {
        public LpsParser Required { get; }

        public LpsParser Optional { get; }

        public LpsParser OptionalMultiple { get; }

        public NewLineParser()
        {
            Required = Lp.ZeroOrMore(c => c == ' ' || c == '\t') + Lp.OneOrMore(c => c == '\n' || c == '\r') + Lp.ZeroOrMore(c => c == ' ' || c == '\t');
            Optional = Lp.ZeroOrMore(c => c == ' ' || c == '\t') + Lp.ZeroOrMore(c => c == '\n' || c == '\r') + Lp.ZeroOrMore(c => c == ' ' || c == '\t');

            OptionalMultiple = Lp.ZeroOrMore(c => c == ' ' || c == '\n' || c == '\r' || c == '\t');
            //_optionalMultiple = (Lp.ZeroOrMore(c => c == ' ') + Lp.ZeroOrMore(c => c == '\n') + Lp.ZeroOrMore(c => c == ' ')).ZeroOrMore().Maybe()
        }
    }
}