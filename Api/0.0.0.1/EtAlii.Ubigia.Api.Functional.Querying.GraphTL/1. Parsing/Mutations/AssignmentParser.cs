namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal class AssignmentParser : IAssignmentParser
    {
        public LpsParser Parser { get; }

        public AssignmentParser()
        {
            var whitespace = Lp.ZeroOrMore(c => c == ' ' || c == '\t');
            Parser = whitespace + Lp.Char('<') + Lp.Char('=') + whitespace;
        }
    }
}