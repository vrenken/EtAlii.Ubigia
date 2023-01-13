// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context;

using System;
using System.Linq;
using EtAlii.Ubigia.Api.Functional.Traversal;
using Moppet.Lapa;

internal class AddAndSelectMultipleNodesAnnotationParser : IAddAndSelectMultipleNodesAnnotationParser
{
    public string Id => nameof(AddAndSelectMultipleNodesAnnotation);
    public LpsParser Parser { get; }

    private const string SourceId = "Source";
    private const string NameId = "Name";

    private readonly INodeValidator _nodeValidator;
    private readonly INodeFinder _nodeFinder;
    private readonly INonRootedPathSubjectParser _nonRootedPathSubjectParser;
    private readonly IRootedPathSubjectParser _rootedPathSubjectParser;
    private readonly IQuotedTextParser _quotedTextParser;

    public AddAndSelectMultipleNodesAnnotationParser(
        INodeValidator nodeValidator,
        INodeFinder nodeFinder,
        INonRootedPathSubjectParser nonRootedPathSubjectParser,
        IRootedPathSubjectParser rootedPathSubjectParser,
        IWhitespaceParser whitespaceParser,
        IQuotedTextParser quotedTextParser
    )
    {
        _nodeValidator = nodeValidator;
        _nodeFinder = nodeFinder;
        _nonRootedPathSubjectParser = nonRootedPathSubjectParser;
        _rootedPathSubjectParser = rootedPathSubjectParser;
        _quotedTextParser = quotedTextParser;

        // @nodes-add(PATH, NAME)
        var sourceParser = new LpsParser(SourceId, true, rootedPathSubjectParser.Parser | nonRootedPathSubjectParser.Parser);
        var nameParser = new LpsParser(NameId, true, Lp.Name().Wrap(NameId) | _quotedTextParser.Parser);

        Parser = new LpsParser(Id, true, "@" + AnnotationPrefix.NodesAdd + "(" + sourceParser + whitespaceParser.Optional + "," + whitespaceParser.Optional + nameParser + ")");
    }

    public NodeAnnotation Parse(LpNode node)
    {
        _nodeValidator.EnsureSuccess(node, Id);

        var sourceNode = _nodeFinder.FindFirst(node, SourceId);
        var sourceChildNode = sourceNode.Children.Single();
        var sourcePath = sourceChildNode.Id switch
        {
            { } id when id == _rootedPathSubjectParser.Id => (PathSubject) _rootedPathSubjectParser.Parse(sourceChildNode),
            { } id when id == _nonRootedPathSubjectParser.Id => (PathSubject) _nonRootedPathSubjectParser.Parse(sourceChildNode),
            _ => throw new NotSupportedException($"Cannot find path subject in: {node.Match}")
        };

        var nameNode = _nodeFinder.FindFirst(node, NameId);
        var nameChildNode = nameNode.Children.Single();
        var name = nameChildNode.Id switch
        {
            {} id when id == _quotedTextParser.Id => _quotedTextParser.Parse(nameChildNode),
            _ => nameChildNode.Match.ToString()
        };

        var identity = name.StartsWith("$")
            ? new NodeIdentity { Name = name[1..], IsVariable = true }
            : new NodeIdentity { Name = name, IsVariable = false };

        return new AddAndSelectMultipleNodesAnnotation(sourcePath, identity);
    }

    public bool CanParse(LpNode node)
    {
        return node.Id == Id;
    }

    public void Validate(StructureFragment parent, StructureFragment self, NodeAnnotation annotation, int depth)
    {
        throw new NotImplementedException();
    }

    public bool CanValidate(NodeAnnotation annotation)
    {
        return annotation is AddAndSelectMultipleNodesAnnotation;
    }
}
