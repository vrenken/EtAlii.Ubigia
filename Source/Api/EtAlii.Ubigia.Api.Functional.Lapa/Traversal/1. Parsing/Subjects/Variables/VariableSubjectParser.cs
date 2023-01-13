// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using Moppet.Lapa;

internal sealed class VariableSubjectParser : IVariableSubjectParser
{
    public string Id => nameof(VariableSubject);

    public LpsParser Parser { get; }

    private readonly INodeValidator _nodeValidator;
    private readonly INodeFinder _nodeFinder;
    private const string TextId = "Text";

    public VariableSubjectParser(
        INodeValidator nodeValidator,
        INodeFinder nodeFinder)
    {
        _nodeValidator = nodeValidator;
        _nodeFinder = nodeFinder;
        Parser = new LpsParser(Id, true, Lp.Char('$') + Lp.LetterOrDigit().OneOrMore().Id(TextId));
    }

    public Subject Parse(LpNode node)
    {
        _nodeValidator.EnsureSuccess(node, Id);
        var text = _nodeFinder.FindFirst(node, TextId).Match.ToString();
        return new VariableSubject(text);
    }

    public bool CanParse(LpNode node)
    {
        return node.Id == Id;
    }
}
