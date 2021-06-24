// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Moppet.Lapa;

    internal class SetAndSelectValueAnnotationParser : ISetAndSelectValueAnnotationParser
    {
        public string Id => nameof(AssignAndSelectValueAnnotation);
        public LpsParser Parser { get; }

        private const string SourceId = "Source";
        private const string NameId = "Name";

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private readonly INonRootedPathSubjectParser _nonRootedPathSubjectParser;
        private readonly IRootedPathSubjectParser _rootedPathSubjectParser;
        private readonly IQuotedTextParser _quotedTextParser;

        public SetAndSelectValueAnnotationParser(
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

            // @node-set(SOURCE, VALUE)
            var sourceParser = new LpsParser(SourceId, true, rootedPathSubjectParser.Parser | nonRootedPathSubjectParser.Parser);
            var nameParser = new LpsParser(NameId, true, Lp.Name().Wrap(NameId) | Lp.OneOrMore(c => char.IsLetterOrDigit(c)).Wrap(NameId) | _quotedTextParser.Parser);

            Parser = new LpsParser(Id, true, "@" + AnnotationPrefix.ValueSet + "(" +
                                             whitespaceParser.Optional + sourceParser + whitespaceParser.Optional + ("," + whitespaceParser.Optional + nameParser + whitespaceParser.Optional).Maybe() + ")");
        }

        public ValueAnnotation Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);

            var sourceNode = _nodeFinder.FindFirst(node, SourceId);
            var sourceChildNode = sourceNode.Children.Single();
            var sourcePath = sourceChildNode.Id switch
            {
                { } id when id == _rootedPathSubjectParser.Id => _rootedPathSubjectParser.Parse(sourceChildNode),
                { } id when id == _nonRootedPathSubjectParser.Id => _nonRootedPathSubjectParser.Parse(sourceChildNode),
                _ => throw new NotSupportedException($"Cannot find path subject in: {node.Match}")
            };

            Subject name;
            var nameNode = _nodeFinder.FindFirst(node, NameId);
            if (nameNode != null)
            {
                var nameChildNode = nameNode.Children.Single();
                var nameString = nameChildNode.Id switch
                {
                    {} id when id == _quotedTextParser.Id => _quotedTextParser.Parse(nameChildNode),
                    _ => nameChildNode.Match.ToString()
                };
                name = nameString != null ? new StringConstantSubject(nameString) : null;
            }
            else
            {
                name = sourcePath; // We have one single argument - let's assume it is the name and not the source path.
                sourcePath = null;
            }

            return new AssignAndSelectValueAnnotation((PathSubject)sourcePath, name);
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
            return annotation is AssignAndSelectValueAnnotation;
        }
    }
}
