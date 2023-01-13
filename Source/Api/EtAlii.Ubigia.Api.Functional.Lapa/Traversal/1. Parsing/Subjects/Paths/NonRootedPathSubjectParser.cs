// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;
using System.Linq;
using Moppet.Lapa;

internal sealed class NonRootedPathSubjectParser : INonRootedPathSubjectParser
{
    public string Id => nameof(NonRootedPathSubject);
    public LpsParser Parser { get; }

    private readonly INodeValidator _nodeValidator;
    private readonly IPathSubjectPartsParser _pathSubjectPartsParser;

    public NonRootedPathSubjectParser(
        INodeValidator nodeValidator,
        IPathSubjectPartsParser pathSubjectPartsParser)
    {
        _nodeValidator = nodeValidator;
        _pathSubjectPartsParser = pathSubjectPartsParser;
        Parser = new LpsParser
        (
            Id, true,
            _pathSubjectPartsParser.Parser.OneOrMore() +
            Lp.Lookahead(Lp.Not("."))
        );//.Debug("PathSubjectParser", true)
    }

    public Subject Parse(LpNode node)
    {
        _nodeValidator.EnsureSuccess(node, Id);
        var childNodes = node.Children ?? Array.Empty<LpNode>();
        var parts = childNodes.Select(childNode => _pathSubjectPartsParser.Parse(childNode)).ToArray();

        // A relative path with the length of 1 should not be parsed as a path but as a string constant.
        Subject result;
        var lengthIsOne = parts.Length == 1;
        if (lengthIsOne && parts[0] is ConstantPathSubjectPart)
        {
            result = new StringConstantSubject(((ConstantPathSubjectPart)parts[0]).Name);
        }
        else if (lengthIsOne && parts[0] is VariablePathSubjectPart)
        {
            result = new VariableSubject(((VariablePathSubjectPart)parts[0]).Name);
        }
        else
        {
            result = parts[0] is ParentPathSubjectPart
                ? new AbsolutePathSubject(parts)
                : new RelativePathSubject(parts);
        }
        return result;
    }


    public bool CanParse(LpNode node)
    {
        return node.Id == Id;
    }
}
