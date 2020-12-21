namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using Moppet.Lapa;

    internal class SetAndSelectNodeValueAnnotationParser : ISetAndSelectNodeValueAnnotationParser
    {
        public string Id { get; } = nameof(AssignAndSelectNodeValueAnnotation);
        public LpsParser Parser { get; }

        private const string _sourceId = "Source";
        private const string _nameId = "Name";
        
        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private readonly INonRootedPathSubjectParser _nonRootedPathSubjectParser;
        private readonly IRootedPathSubjectParser _rootedPathSubjectParser;
        private readonly IQuotedTextParser _quotedTextParser;

        public SetAndSelectNodeValueAnnotationParser(
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
            var sourceParser = new LpsParser(_sourceId, true, rootedPathSubjectParser.Parser | nonRootedPathSubjectParser.Parser);
            var nameParser = new LpsParser(_nameId, true, Lp.Name().Wrap(_nameId) | Lp.OneOrMore(c => char.IsLetterOrDigit(c)).Wrap(_nameId) | _quotedTextParser.Parser);
            
            Parser = new LpsParser(Id, true, "@" + AnnotationPrefix.NodeValueSet + "(" + 
                                             whitespaceParser.Optional + sourceParser + whitespaceParser.Optional + ("," + whitespaceParser.Optional + nameParser + whitespaceParser.Optional).Maybe() + ")");
        }

        public NodeValueAnnotation Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);

            var sourceNode = _nodeFinder.FindFirst(node, _sourceId);
            var sourceChildNode = sourceNode.Children.Single();
            var sourcePath = sourceChildNode.Id switch
            {
                { } id when id == _rootedPathSubjectParser.Id => _rootedPathSubjectParser.Parse(sourceChildNode),
                { } id when id == _nonRootedPathSubjectParser.Id => _nonRootedPathSubjectParser.Parse(sourceChildNode),
                _ => throw new NotSupportedException($"Cannot find path subject in: {node.Match}")
            };

            Subject name;
            var nameNode = _nodeFinder.FindFirst(node, _nameId);
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

            return new AssignAndSelectNodeValueAnnotation((PathSubject)sourcePath, name);
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public void Validate(StructureFragment parent, StructureFragment self, NodeValueAnnotation annotation, int depth)
        {
            throw new NotImplementedException();
        }

        public bool CanValidate(NodeValueAnnotation annotation)
        {
            return annotation is AssignAndSelectNodeValueAnnotation;
        }
    }
}