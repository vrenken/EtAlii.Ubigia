// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context;

using System;
using System.Linq;
using EtAlii.Ubigia.Api.Functional.Traversal;
using Moppet.Lapa;

internal sealed class ClearAndSelectValueAnnotationParser : IClearAndSelectValueAnnotationParser
{
    public string Id => nameof(ClearAndSelectValueAnnotation);
    public LpsParser Parser { get; }

    private const string SourceId = "Source";

    private readonly INodeValidator _nodeValidator;
    private readonly INodeFinder _nodeFinder;
    private readonly INonRootedPathSubjectParser _nonRootedPathSubjectParser;
    private readonly IRootedPathSubjectParser _rootedPathSubjectParser;

    public ClearAndSelectValueAnnotationParser(
        INodeValidator nodeValidator,
        INodeFinder nodeFinder,
        INonRootedPathSubjectParser nonRootedPathSubjectParser,
        IRootedPathSubjectParser rootedPathSubjectParser,
        IWhitespaceParser whitespaceParser)
    {
        _nodeValidator = nodeValidator;
        _nodeFinder = nodeFinder;
        _nonRootedPathSubjectParser = nonRootedPathSubjectParser;
        _rootedPathSubjectParser = rootedPathSubjectParser;

        // @value-clear(SOURCE)
        var sourceParser = new LpsParser(SourceId, true, rootedPathSubjectParser.Parser | nonRootedPathSubjectParser.Parser);

        Parser = new LpsParser(Id, true, "@" + AnnotationPrefix.ValueClear + "(" + whitespaceParser.Optional + sourceParser.Maybe() + whitespaceParser.Optional + ")");
    }

    public ValueAnnotation Parse(LpNode node)
    {
        _nodeValidator.EnsureSuccess(node, Id);

        Subject path = null;
        var sourceNode = _nodeFinder.FindFirst(node, SourceId);
        if (sourceNode != null)
        {
            var sourceChildNode = sourceNode.Children.Single();
            path = sourceChildNode.Id switch
            {
                { } id when id == _rootedPathSubjectParser.Id => _rootedPathSubjectParser.Parse(sourceChildNode),
                { } id when id == _nonRootedPathSubjectParser.Id => _nonRootedPathSubjectParser.Parse(sourceChildNode),
                _ => throw new NotSupportedException($"Cannot find path subject in: {node.Match}")
            };
        }

        var sourcePath = path switch
        {
            { } p when p is PathSubject pathSubject => pathSubject,
            { } p when p is StringConstantSubject stringConstantSubject => new RelativePathSubject(new ConstantPathSubjectPart(stringConstantSubject.Value)),
            null => null,
            _ => throw new NotSupportedException($"Cannot convert path subject in: {node.Match}")
        };

        return new ClearAndSelectValueAnnotation(sourcePath);
    }

    public bool CanParse(LpNode node)
    {
        return node.Id == Id;
    }

    public void Validate(StructureFragment parent, StructureFragment self, ValueAnnotation annotation, int depth)
    {
        throw new NotImplementedException();
    }

    public bool CanValidate(ValueAnnotation annotation)
    {
        return annotation is ClearAndSelectValueAnnotation;
    }
}
