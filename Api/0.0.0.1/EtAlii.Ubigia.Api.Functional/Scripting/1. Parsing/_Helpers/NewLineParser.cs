namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal class NewLineParser : INewLineParser
    {
        public LpsChain Required => _required;
        private readonly LpsChain _required;

        public LpsChain Optional => _optional;
        private readonly LpsChain _optional;

        public LpsParser OptionalMultiple => _optionalMultiple;
        private readonly LpsParser _optionalMultiple;

        public NewLineParser()
        {
            _required = Lp.ZeroOrMore(c => c == ' ') + Lp.OneOrMore(c => c == '\n') + Lp.ZeroOrMore(c => c == ' ');
            _optional = Lp.ZeroOrMore(c => c == ' ') + Lp.ZeroOrMore(c => c == '\n') + Lp.ZeroOrMore(c => c == ' ');

            _optionalMultiple = Lp.ZeroOrMore(c => c == ' ' || c == '\n');
            //_optionalMultiple = (Lp.ZeroOrMore(c => c == ' ') + Lp.ZeroOrMore(c => c == '\n') + Lp.ZeroOrMore(c => c == ' ')).ZeroOrMore().Maybe();
        }
    }
}