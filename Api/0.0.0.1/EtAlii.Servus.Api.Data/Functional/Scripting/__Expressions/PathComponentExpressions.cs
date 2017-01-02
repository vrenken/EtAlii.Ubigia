namespace EtAlii.Servus.Api.Data
{
    using Moppet.Lapa;

    public class PathComponentExpressions
    {
        public readonly LpsParser Variable;
        public readonly LpsParser Name;
        public readonly LpsParser Identifier;
        public readonly LpsParser Wildcard;

        internal PathComponentExpressions(
            TerminalExpressions terminalExpressions,
            OperatorExpressions operatorExpressions)
        {
            Name = new LpsParser
            {
                Parser = Lp.Char('(') + terminalExpressions.Symbol + Lp.Char(')') |
                         terminalExpressions.Symbol
            };

            Identifier = new LpsParser
            {
                Parser = Lp.Char('(') + terminalExpressions.Identifier + Lp.Char(')') |
                         terminalExpressions.Identifier
            };

            Identifier = new LpsParser
            {
                Parser = Lp.Char('(') + terminalExpressions.Variable + Lp.Char(')') |
                         terminalExpressions.Variable
            };

            Wildcard = new LpsParser
            {
                Parser = Lp.Term("()") |
                         Lp.Term("(*)") |
                         Lp.Term("*")
            };
        }

    }
}
