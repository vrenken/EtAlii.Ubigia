// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context;

using EtAlii.Ubigia.Api.Functional.Traversal;
using Moppet.Lapa;

internal class RequirementParser : IRequirementParser
{
    public LpsParser Parser { get; }

    public string Id => nameof(Requirement);

    private readonly INodeValidator _nodeValidator;

    public RequirementParser(INodeValidator nodeValidator)
    {
        _nodeValidator = nodeValidator;
        Parser = new LpsParser(Id, true, Lp.Char('!') | Lp.Char('?')).Maybe();
    }

    public Requirement Parse(LpNode node)
    {
        _nodeValidator.EnsureSuccess(node, Id);

        var requirement = Requirement.None;
        var match = node.ToString();
        return match switch
        {
            "?" => Requirement.Optional,
            "!" => Requirement.Mandatory,
            _ => requirement
        };
    }
}
