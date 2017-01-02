namespace EtAlii.Servus.Api.Data
{
    using System.Linq;
    using Moppet.Lapa;
    using System;

    public class OperatorExpressions
    {

        public readonly LpsParser Assign;
        public readonly LpsParser Add;
        public readonly LpsParser Remove;
        public readonly LpsParser All;

        public OperatorExpressions(TerminalExpressions terminalExpressions)
        {
            Assign = terminalExpressions.PossibleWhitespace + Lp.One(c => c == '<') + Lp.One(c => c == '=') + terminalExpressions.PossibleWhitespace;
            Add = terminalExpressions.PossibleWhitespace + Lp.One(c => c == '+') + Lp.One(c => c == '=') + terminalExpressions.PossibleWhitespace;
            Remove = terminalExpressions.PossibleWhitespace + Lp.One(c => c == '-') + Lp.One(c => c == '=') + terminalExpressions.PossibleWhitespace;

            All = new LpsParser(Assign | Add | Remove);
        }
    }
}
