// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System.Linq;
using Moppet.Lapa;

internal sealed class ConstantSubjectsParser : IConstantSubjectsParser
{
    public string Id => "ConstantSubjects";

    public LpsParser Parser { get; }

    private readonly INodeValidator _nodeValidator;
    private readonly IConstantSubjectParser[] _parsers;

    public ConstantSubjectsParser(
        IStringConstantSubjectParser stringConstantSubjectParser,
        IObjectConstantSubjectParser objectConstantSubjectParser,
        INodeValidator nodeValidator)
    {
        _parsers = new IConstantSubjectParser[]
        {
            stringConstantSubjectParser,
            objectConstantSubjectParser,
        };
        _nodeValidator = nodeValidator;
        var lpsParsers = _parsers.Aggregate(new LpsAlternatives(), (current, parser) => current | parser.Parser);
        Parser = new LpsParser(Id, true, lpsParsers);//.Debug("ConstantSubjectsParser", true)
    }

    public Subject Parse(LpNode node)
    {
        _nodeValidator.EnsureSuccess(node, Id);
        var childNode = node.Children.Single();
        var parser = _parsers.Single(p => p.CanParse(childNode));
        var result = parser.Parse(childNode);
        return result;
    }


    public bool CanParse(LpNode node)
    {
        return node.Id == Id;
    }
}
