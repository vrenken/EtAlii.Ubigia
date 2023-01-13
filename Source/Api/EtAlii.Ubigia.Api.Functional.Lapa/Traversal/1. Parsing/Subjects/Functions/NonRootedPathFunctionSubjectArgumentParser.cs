// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;
using System.Linq;
using Moppet.Lapa;

internal sealed class NonRootedPathFunctionSubjectArgumentParser : INonRootedPathFunctionSubjectArgumentParser
{
    public string Id => nameof(NonRootedPathFunctionSubjectArgument);

    public LpsParser Parser { get; }

    private readonly INodeValidator _nodeValidator;
    private readonly IPathSubjectPartsParser _pathSubjectPartsParser;

    public NonRootedPathFunctionSubjectArgumentParser(
        INodeValidator nodeValidator,
        IPathSubjectPartsParser pathSubjectPartsParser)
    {
        _nodeValidator = nodeValidator;
        _pathSubjectPartsParser = pathSubjectPartsParser;
        Parser = new LpsParser(Id, true, _pathSubjectPartsParser.Parser.OneOrMore());
    }

    public FunctionSubjectArgument Parse(LpNode node)
    {
        _nodeValidator.EnsureSuccess(node, Id);
        var childNodes = node.Children ?? Array.Empty<LpNode>();
        var parts = childNodes.Select(childNode => _pathSubjectPartsParser.Parse(childNode)).ToArray();

        var subject = parts[0] is ParentPathSubjectPart
            ? new AbsolutePathSubject(parts)
            : (NonRootedPathSubject)new RelativePathSubject(parts);

        return new NonRootedPathFunctionSubjectArgument(subject);
    }

    public bool CanParse(LpNode node)
    {
        return node.Id == Id;
    }
}
