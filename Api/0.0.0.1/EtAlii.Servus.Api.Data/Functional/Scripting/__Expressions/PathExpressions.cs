namespace EtAlii.Servus.Api.Data
{
    using Moppet.Lapa;

    public class PathExpressions
    {
        public readonly LpsParser Rooted;
        public readonly LpsParser NonRooted;

        internal PathExpressions(
            TerminalExpressions terminalExpressions,
            OperatorExpressions operatorExpressions)
        {
            // /[COMPONENT]/[SYMBOL_OR_VAR_OR_IDENTIFIER]
            var pathComponentExpression = new LpsParser(NonTerminalId.PathComponent, true, terminalExpressions.Symbol | terminalExpressions.Variable | terminalExpressions.Identifier);
            Rooted = (terminalExpressions.Separator + pathComponentExpression).OneOrMore();
            
            
            // [COMPONENT]/[SYMBOL_OR_VAR_OR_IDENTIFIER]
            var nonRootedPathComponentExpression = new LpsParser(NonTerminalId.PathComponent, true, terminalExpressions.Symbol);
            NonRooted = (nonRootedPathComponentExpression + terminalExpressions.Separator).ZeroOrMore() + nonRootedPathComponentExpression;
        }
    }
}
