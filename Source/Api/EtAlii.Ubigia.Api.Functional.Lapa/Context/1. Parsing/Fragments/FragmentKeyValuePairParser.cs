// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context;

using EtAlii.Ubigia.Api.Functional.Traversal;
using Moppet.Lapa;

internal class FragmentKeyValuePairParser : KeyValuePairParserBase, IFragmentKeyValuePairParser
{
    private readonly IAssignmentParser _assignmentParser;
    string IFragmentKeyValuePairParser.Id => Id;

    // SONARQUBE_DependencyInjectionSometimesRequiresMoreThan7Parameters:
    // After a (very) long period of considering all options I am convinced that we won't be able to break down all DI patterns so that they fit within the 7 limit
    // specified by SonarQube. The current setup here is already some kind of facade that hides away many of the type specific parsing. Therefore refactoring to facades won't work.
    // Therefore this pragma warning disable of S107.
#pragma warning disable S107
    public FragmentKeyValuePairParser(
        INodeValidator nodeValidator,
        IQuotedTextParser quotedTextParser,
        IDateTimeValueParser dateTimeValueParser,
        ITimeSpanValueParser timeSpanValueParser,
        IBooleanValueParser booleanValueParser,
        IIntegerValueParser integerValueParser,
        IFloatValueParser floatValueParser,
        IWhitespaceParser whitespaceParser,
        IAssignmentParser assignmentParser,
        INodeFinder nodeFinder)
        : base(nodeValidator, quotedTextParser, dateTimeValueParser, timeSpanValueParser, booleanValueParser, integerValueParser, floatValueParser, whitespaceParser, nodeFinder)
#pragma warning restore S107
    {
        _assignmentParser = assignmentParser;
    }

    protected override LpsParser InitializeParser()
    {
        var separator = _assignmentParser.Parser;

        return new LpsParser(Id, true,
            (
                Lp.Name().Id(KeyId) |
                QuotedTextParser.Parser.Wrap(KeyId)
            ) +
            separator + new LpsParser(ValueId, true, TypeParsers).Maybe());
    }
}
