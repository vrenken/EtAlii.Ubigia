// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional
{
    using System.Linq;
    using Moppet.Lapa;

    internal class ConstantSubjectsParser : IConstantSubjectsParser
    {
        public string Id { get { return _id; } }
        private readonly string _id = "ConstantSubjects";

        public LpsParser Parser { get { return _parser; } }
        private readonly LpsParser _parser;

        private readonly INodeValidator _nodeValidator;
        private readonly IConstantSubjectParser[] _parsers;
        private const string TextId = "Text";

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
            _parser = new LpsParser(Id, true, lpsParsers);//.Debug("ConstantSubjectsParser", true);
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

        public void Validate(SequencePart before, Subject subject, int subjectIndex, SequencePart after)
        {
            var constantSubject = (ConstantSubject)subject;
            var parser = _parsers.Single(p => p.CanValidate(constantSubject));
            parser.Validate(before, constantSubject, subjectIndex, after);

            if (subjectIndex == 0)
            {
                throw new ScriptParserException("A constant cannot be used as first subject.");
            }
        }

        public bool CanValidate(Subject subject)
        {
            return subject is ConstantSubject;
        }
    }
}
