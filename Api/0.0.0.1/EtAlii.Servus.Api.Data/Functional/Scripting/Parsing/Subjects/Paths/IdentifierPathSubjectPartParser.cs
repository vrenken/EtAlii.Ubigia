namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api.Data.Model;
    using Moppet.Lapa;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class IdentifierPathSubjectPartParser : IPathSubjectPartParser
    {
        public const string Id = "IdentifierPathSubjectPart";
        private const string IdentifierId = "Identifier";

        public LpsParser Parser { get { return _parser; } }
        private readonly LpsParser _parser;
        private readonly IParserHelper _parserHelper;
        private const char IdentifierSeparatorCharacter = '.';

        public IdentifierPathSubjectPartParser(IParserHelper parserHelper)
        {
            _parserHelper = parserHelper;

            var identifierParser = CreateIdentifierParser();
            _parser = new LpsParser(Id, true, Lp.Char('&') + identifierParser.Id(IdentifierId));
        }

        private LpsParser CreateIdentifierParser()
        {
            char[] hexDigits = new[] { 'a', 'b', 'c', 'd', 'e', 'f' };
            var hexChar = Lp.One(c => char.IsDigit(c) || hexDigits.Contains(char.ToLower(c)));
            var hexSeparator = Lp.Char('-');
            var identifierSeparator = Lp.Char(IdentifierSeparatorCharacter);
            var guid = (hexChar + hexChar + hexChar + hexChar + hexChar + hexChar + hexChar + hexChar + hexSeparator +
                        hexChar + hexChar + hexChar + hexChar + hexSeparator +
                        hexChar + hexChar + hexChar + hexChar + hexSeparator +
                        hexChar + hexChar + hexChar + hexChar + hexSeparator +
                        hexChar + hexChar + hexChar + hexChar + hexChar + hexChar + hexChar + hexChar + hexChar + hexChar + hexChar + hexChar) |
                       (hexChar + hexChar + hexChar + hexChar + hexChar + hexChar + hexChar + hexChar +
                        hexChar + hexChar + hexChar + hexChar +
                        hexChar + hexChar + hexChar + hexChar +
                        hexChar + hexChar + hexChar + hexChar +
                        hexChar + hexChar + hexChar + hexChar + hexChar + hexChar + hexChar + hexChar + hexChar + hexChar + hexChar + hexChar);

            // [GUID].[GUID].[GUID].[ULONG].[ULONG].[ULONG]
            var identifierParser = 
            (
                guid + identifierSeparator +
                guid + identifierSeparator +
                guid + identifierSeparator +
                Lp.OneOrMore(c => char.IsDigit(c)) + identifierSeparator +
                Lp.OneOrMore(c => char.IsDigit(c)) + identifierSeparator +
                Lp.OneOrMore(c => char.IsDigit(c))
            );

            return identifierParser;
        }

        public PathSubjectPart Parse(LpNode node)
        {
            _parserHelper.EnsureSuccess2(node, Id);
            var identifier = GetIdentifier(node);
            return new IdentifierPathSubjectPart(identifier);
        }

        private Identifier GetIdentifier(LpNode node)
        {
            var identifierNode = _parserHelper.FindFirst(node, IdentifierId);
            var pieces = identifierNode.Match.ToString().Split(IdentifierSeparatorCharacter);
            var storage = Guid.Parse(pieces[0]);
            var account = Guid.Parse(pieces[1]);
            var space = Guid.Parse(pieces[2]);
            var era = ulong.Parse(pieces[3]);
            var period = ulong.Parse(pieces[4]);
            var moment = ulong.Parse(pieces[5]);

            var identifier = Identifier.Create(storage, account, space, era, period, moment);
            return identifier;
        }

        public void Validate(PathSubjectPart before, PathSubjectPart part, int partIndex, PathSubjectPart after)
        {
            if ((before == null || before is IsParentOfPathSubjectPart) && partIndex <= 1)
            {
            }
            else
            {
                throw new ScriptParserException("A identifier path part can only be used at the start of a path");
            }
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public bool CanValidate(PathSubjectPart part)
        {
            return part is IdentifierPathSubjectPart;
        }
    }
}
