// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Moppet.Lapa;

    internal class NodeAnnotationsParser : INodeAnnotationsParser
    {
        public string Id => nameof(NodeAnnotation);

        public LpsParser Parser { get; }

        private readonly INodeAnnotationParser[] _parsers;
        private readonly INodeValidator _nodeValidator;

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
}
