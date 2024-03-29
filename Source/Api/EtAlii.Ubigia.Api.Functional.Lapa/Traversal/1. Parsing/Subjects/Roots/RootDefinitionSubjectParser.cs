﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using Moppet.Lapa;

internal sealed class RootDefinitionSubjectParser : IRootDefinitionSubjectParser
{
    public string Id => nameof(RootDefinitionSubject);

    public LpsParser Parser { get; }

    private readonly INodeValidator _nodeValidator;
    private readonly INodeFinder _nodeFinder;
    private readonly ITypeValueParser _typeValueParser;

    public RootDefinitionSubjectParser(
        INodeValidator nodeValidator,
        INodeFinder nodeFinder,
        ITypeValueParser typeValueParser
    )
    {
        _nodeValidator = nodeValidator;
        _nodeFinder = nodeFinder;
        _typeValueParser = typeValueParser;

        Parser = new LpsParser
        (
            Id, true,
            _typeValueParser.Parser + //.Debug("TypeValueParser", true) //+
            (
                Lp.End //|
                //(Lp.Char(':') + _pathSubjectPartsParser.Parser.OneOrMore().Wrap(PathId))
            )//.Debug("RootDefinitionSubjectParser-inner", true)
        );//.Debug("RootDefinitionSubjectParser", true)
    }

    public Subject Parse(LpNode node)
    {
        _nodeValidator.EnsureSuccess(node, Id);
        var quotedTextNode = _nodeFinder.FindFirst(node, _typeValueParser.Id);
        var type = _typeValueParser.Parse(quotedTextNode);

        return new RootDefinitionSubject(new RootType(type));
    }

    public bool CanParse(LpNode node)
    {
        return node.Id == Id;
    }
}
