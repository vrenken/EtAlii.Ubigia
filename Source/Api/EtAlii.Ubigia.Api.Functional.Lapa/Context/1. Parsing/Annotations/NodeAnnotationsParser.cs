// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context;

using System.Linq;
using EtAlii.Ubigia.Api.Functional.Traversal;
using Moppet.Lapa;

internal class NodeAnnotationsParser : INodeAnnotationsParser
{
    public string Id => nameof(NodeAnnotation);

    public LpsParser Parser { get; }

    private readonly INodeAnnotationParser[] _parsers;
    private readonly INodeValidator _nodeValidator;

    // SONARQUBE_DependencyInjectionSometimesRequiresMoreThan7Parameters:
    // After a (very) long period of considering all options I am convinced that we won't be able to break down all DI patterns so that they fit within the 7 limit
    // specified by SonarQube. The current setup here is already some kind of facade that hides away many specific parsing variations. Therefore refactoring to facades won't work.
    // Therefore this pragma warning disable of S107.
#pragma warning disable S107
    public NodeAnnotationsParser(
        INodeValidator nodeValidator,
        IAddAndSelectMultipleNodesAnnotationParser addAndSelectMultipleNodesAnnotationParser,
        IAddAndSelectSingleNodeAnnotationParser addAndSelectSingleNodeAnnotationParser,
        ILinkAndSelectMultipleNodesAnnotationParser linkAndSelectMultipleNodesAnnotationParser,
        ILinkAndSelectSingleNodeAnnotationParser linkAndSelectSingleNodeAnnotationParser,
        IRemoveAndSelectMultipleNodesAnnotationParser removeAndSelectMultipleNodesAnnotationParser,
        IRemoveAndSelectSingleNodeAnnotationParser removeAndSelectSingleNodeAnnotationParser,
        ISelectMultipleNodesAnnotationParser selectMultipleNodesAnnotationParser,
        ISelectSingleNodeAnnotationParser selectSingleNodeAnnotationParser,
        IUnlinkAndSelectMultipleNodesAnnotationParser unlinkAndSelectMultipleNodesAnnotationParser,
        IUnlinkAndSelectSingleNodeAnnotationParser unlinkAndSelectSingleNodeAnnotationParser)
#pragma warning restore S107
    {
        _parsers = new INodeAnnotationParser[]
        {
            addAndSelectMultipleNodesAnnotationParser,
            addAndSelectSingleNodeAnnotationParser,
            linkAndSelectMultipleNodesAnnotationParser,
            linkAndSelectSingleNodeAnnotationParser,
            removeAndSelectMultipleNodesAnnotationParser,
            removeAndSelectSingleNodeAnnotationParser,
            selectMultipleNodesAnnotationParser,
            selectSingleNodeAnnotationParser,
            unlinkAndSelectMultipleNodesAnnotationParser,
            unlinkAndSelectSingleNodeAnnotationParser,
        };

        _nodeValidator = nodeValidator;
        var lpsParsers = _parsers.Aggregate(new LpsAlternatives(), (current, parser) => current | parser.Parser);
        Parser = new LpsParser(Id, true, lpsParsers);
    }

    public NodeAnnotation Parse(LpNode node)
    {
        _nodeValidator.EnsureSuccess(node, Id);
        var childNode = node.Children.Single();
        var parser = _parsers.Single(p => p.CanParse(childNode));
        var result = parser.Parse(childNode);
        return result;
    }

    public bool CanParse(LpNode node)
    {
        throw new System.NotImplementedException();
    }

    public void Validate(StructureFragment parent, StructureFragment self, NodeAnnotation annotation, int depth)
    {
        var parser = _parsers.Single(p => p.CanValidate(annotation));
        parser.Validate(parent, self, annotation, depth);
    }

    public bool CanValidate(Annotation annotation)
    {
        return annotation is NodeAnnotation;
    }
}
