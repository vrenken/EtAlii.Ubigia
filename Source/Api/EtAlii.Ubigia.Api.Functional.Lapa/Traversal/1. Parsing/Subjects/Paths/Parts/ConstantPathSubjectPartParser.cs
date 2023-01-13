// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using Moppet.Lapa;

internal sealed class ConstantPathSubjectPartParser : IConstantPathSubjectPartParser
{
    public string Id => nameof(ConstantPathSubjectPart);

    public LpsParser Parser { get; }

    private readonly INodeValidator _nodeValidator;
    private readonly INodeFinder _nodeFinder;
    private const string TextId = "Text";

    public ConstantPathSubjectPartParser(
        INodeValidator nodeValidator,
        IConstantHelper constantHelper,
        INodeFinder nodeFinder)
    {
        _nodeValidator = nodeValidator;
        _nodeFinder = nodeFinder;

        Parser = new LpsParser(Id, true,
            (Lp.One(constantHelper.IsValidConstantCharacter).OneOrMore().Id(TextId)) |
            (Lp.One(c => c == '\"') + Lp.One(c => constantHelper.IsValidQuotedConstantCharacter(c, '\"')).ZeroOrMore().Id(TextId) + Lp.One(c => c == '\"')) |
            (Lp.One(c => c == '\'') + Lp.One(c => constantHelper.IsValidQuotedConstantCharacter(c, '\'')).ZeroOrMore().Id(TextId) + Lp.One(c => c == '\''))
        );
    }

    public bool CanParse(LpNode node)
    {
        return node.Id == Id;
    }

    public PathSubjectPart Parse(LpNode node)
    {
        _nodeValidator.EnsureSuccess(node, Id);
        var text = _nodeFinder.FindFirst(node, TextId).Match.ToString();
        return new ConstantPathSubjectPart(text);
    }
}
