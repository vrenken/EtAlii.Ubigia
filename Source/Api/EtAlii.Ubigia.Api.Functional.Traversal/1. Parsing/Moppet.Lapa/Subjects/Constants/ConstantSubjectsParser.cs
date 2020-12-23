// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;
    using Moppet.Lapa;

    internal class ConstantSubjectsParser : IConstantSubjectsParser
    {
        public string Id { get; } = "ConstantSubjects";

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

        public void Validate(SequencePart before, Subject item, int itemIndex, SequencePart after)
        {
            var constantSubject = (ConstantSubject)item;
            var parser = _parsers.Single(p => p.CanValidate(constantSubject));
            parser.Validate(before, constantSubject, itemIndex, after);

            if (itemIndex == 0)
            {
                throw new ScriptParserException("A constant cannot be used as first subject.");
            }
        }

        public bool CanValidate(Subject item)
        {
            return item is ConstantSubject;
        }
    }
}
