// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context;

using System.Linq;
using EtAlii.Ubigia.Api.Functional.Traversal;
using Moppet.Lapa;

internal class ValueFragmentParser : IValueFragmentParser
{
    private readonly IFragmentKeyValuePairParser _keyValuePairParser;
    private readonly INodeValidator _nodeValidator;
    private readonly IQuotedTextParser _quotedTextParser;
    private readonly IValueAnnotationsParser _annotationParser;
    private readonly IRequirementParser _requirementParser;
    private readonly INodeFinder _nodeFinder;

    public LpsParser Parser { get; }

    public string Id => "ValueQuery";

    private const string KeyId = "Key";
    private const string AnnotationId = "Annotation";
    private const string QueryValueId = "QueryValue";

    public ValueFragmentParser(
        INodeValidator nodeValidator,
        IQuotedTextParser quotedTextParser,
        IValueAnnotationsParser annotationParser,
        INodeFinder nodeFinder,
        IFragmentKeyValuePairParser keyValuePairParser,
        IRequirementParser requirementParser,
        IAssignmentParser assignmentParser)
    {
        _nodeValidator = nodeValidator;
        _quotedTextParser = quotedTextParser;
        _annotationParser = annotationParser;
        _nodeFinder = nodeFinder;
        _keyValuePairParser = keyValuePairParser;
        _requirementParser = requirementParser;

        var queryParser = new LpsParser(QueryValueId, true, _requirementParser.Parser + (Lp.Name().Id(KeyId) | _quotedTextParser.Parser.Wrap(KeyId)) + (assignmentParser.Parser + new LpsParser(AnnotationId, true, annotationParser.Parser)).Maybe());

        var mutationKeyValueParser = _keyValuePairParser.Parser;

        Parser = new LpsParser(Id, true, queryParser | mutationKeyValueParser);

    }

    public ValueFragment Parse(LpNode node)
    {
        _nodeValidator.EnsureSuccess(node, Id);

        var child = _nodeFinder.FindFirst(node, Id).Children.Single();

        switch (child.Id)
        {
            case QueryValueId:
                return ParseQueryValue(child);
            case KeyValuePairParserBase.Id:
                var kvpNode = _nodeFinder.FindFirst(child, _keyValuePairParser.Id);
                var kvp = _keyValuePairParser.Parse(kvpNode);
                var prefix = new ValuePrefix(ValueType.Object, Requirement.None);
                var mutation = new PrimitiveMutationValue { Value = kvp.Value };
                return new ValueFragment(kvp.Key, prefix, null, FragmentType.Mutation, mutation);
            default:
                throw new SchemaParserException($"Unable to find correctly formatted {nameof(ValueFragment)}.");
        }
    }

    private ValueFragment ParseQueryValue(LpNode node)
    {
        var requirementNode = _nodeFinder.FindFirst(node, _requirementParser.Id);
        var requirement = requirementNode != null ? _requirementParser.Parse(requirementNode) : Requirement.None;

        var nameNode = _nodeFinder.FindFirst(node, KeyId);
        var constantNode = nameNode.FirstOrDefault(n => n.Id == _quotedTextParser.Id);
        var name = constantNode == null ? nameNode.Match.ToString() : _quotedTextParser.Parse(constantNode);

        var annotationNode = _nodeFinder.FindFirst(node, AnnotationId);
        ValueAnnotation annotation = null;
        if (annotationNode != null)
        {
            var annotationValueNode = _nodeFinder.FindFirst(annotationNode, _annotationParser.Id);
            annotation = _annotationParser.Parse(annotationValueNode);
        }

        var fragmentType = annotation == null || annotation is SelectValueAnnotation
            ? FragmentType.Query
            : FragmentType.Mutation;
        var prefix = new ValuePrefix(ValueType.Object, requirement);
        return new ValueFragment(name, prefix, annotation, fragmentType, null);
    }

    public bool CanParse(LpNode node)
    {
        return node.Id == Id;
    }
}
