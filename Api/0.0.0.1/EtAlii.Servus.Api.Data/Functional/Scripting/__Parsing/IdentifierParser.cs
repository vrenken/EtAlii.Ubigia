namespace EtAlii.Servus.Api.Data
{
    using System;
    using Moppet.Lapa;

    internal class IdentifierParser
    {
        private readonly IParserHelper _parserHelper;

        public IdentifierParser(IParserHelper parserHelper)
        {
            _parserHelper = parserHelper;
        }
        
        public Identifier Parse(LpNode node)
        {
            _parserHelper.EnsureSuccess(node, "identifier");

            var identifierNode = _parserHelper.FindFirst(node, TerminalId.Identifier);
            var pieces = identifierNode.Match.ToString().Split(TerminalExpressions.IdentifierSeparatorCharacter);
            var storage = Guid.Parse(pieces[0]);
            var account = Guid.Parse(pieces[1]);
            var space = Guid.Parse(pieces[2]);
            var era = ulong.Parse(pieces[3]);
            var period = ulong.Parse(pieces[4]);
            var moment = ulong.Parse(pieces[5]);

            return Identifier.Create(storage, account, space, era, period, moment);
        }

    }
}
