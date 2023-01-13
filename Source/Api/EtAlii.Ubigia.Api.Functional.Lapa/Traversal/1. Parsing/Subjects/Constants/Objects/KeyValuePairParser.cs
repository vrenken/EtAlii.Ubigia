// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using Moppet.Lapa;

internal class KeyValuePairParser : KeyValuePairParserBase, IKeyValuePairParser
{
    string IKeyValuePairParser.Id => Id;

    // SONARQUBE_DependencyInjectionSometimesRequiresMoreThan7Parameters:
    // After a (very) long period of considering all options I am convinced that we won't be able to break down all DI patterns so that they fit within the 7 limit
    // specified by SonarQube. The current setup here is already some kind of facade that hides away many of the type specific parsing. Therefore refactoring to facades won't work.
    // Therefore this pragma warning disable of S107.
#pragma warning disable S107
    public KeyValuePairParser(
        INodeValidator nodeValidator,
        IQuotedTextParser quotedTextParser,
        IDateTimeValueParser dateTimeValueParser,
        ITimeSpanValueParser timeSpanValueParser,
        IBooleanValueParser booleanValueParser,
        IIntegerValueParser integerValueParser,
        IFloatValueParser floatValueParser,
        IWhitespaceParser whitespaceParser,
        INodeFinder nodeFinder)
        : base(nodeValidator, quotedTextParser, dateTimeValueParser, timeSpanValueParser, booleanValueParser, integerValueParser, floatValueParser, whitespaceParser, nodeFinder)
#pragma warning restore S107
    {
    }

    protected override LpsParser InitializeParser()
    {
        var separator = WhitespaceParser.Optional + Lp.Char(':') + WhitespaceParser.Optional;

        return new LpsParser(Id, true,
            (
                Lp.Name().Id(KeyId) |
                QuotedTextParser.Parser.Wrap(KeyId)
            ) +
            separator +
            new LpsParser(ValueId, true, TypeParsers).Maybe());
    }
}
