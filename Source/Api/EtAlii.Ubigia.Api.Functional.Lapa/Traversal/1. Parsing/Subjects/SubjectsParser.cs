// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;
    using Moppet.Lapa;

    internal class SubjectsParser : ISubjectsParser
    {
        public string Id => "Subjects";

        public LpsParser Parser { get; }

        private readonly ISubjectParser[] _parsers;

        public SubjectsParser(
            IFunctionSubjectParser functionSubjectParser,
            IVariableSubjectParser variableSubjectParser,
            IConstantSubjectsParser constantSubjectsParser,
            IRootedPathSubjectParser rootedPathSubjectParser,
            INonRootedPathSubjectParser nonRootedPathSubjectParser,
            IRootSubjectParser rootSubjectParser,
            IRootDefinitionSubjectParser rootDefinitionSubjectParser)
        {
            _parsers = new ISubjectParser[]
            {
                rootSubjectParser,
                functionSubjectParser,
                constantSubjectsParser,
                rootDefinitionSubjectParser,
                rootedPathSubjectParser,
                nonRootedPathSubjectParser,
                variableSubjectParser,
            };
            var lpsParsers = _parsers.Aggregate(new LpsAlternatives(), (current, parser) => current | parser.Parser);
            Parser = new LpsParser(Id, true, lpsParsers);
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public SequencePart Parse(LpNode node, INodeValidator nodeValidator)
        {
            nodeValidator.EnsureSuccess(node, Id);
            var childNode = node.Children.Single();
            var parser = _parsers.Single(p => p.CanParse(childNode));
            var result = parser.Parse(childNode);
            return result;
        }
    }
}
