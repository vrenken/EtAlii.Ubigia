// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal class RootDefinitionSubjectParser : IRootDefinitionSubjectParser
    {
        public string Id { get; } = nameof(RootDefinitionSubject);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private readonly ITypeValueParser _typeValueParser;

        public RootDefinitionSubjectParser(
            INodeValidator nodeValidator,
            INodeFinder nodeFinder,
            ITypeValueParser typeValueParser 
            )
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;
            _typeValueParser = typeValueParser;

            Parser = new LpsParser
                (
                    Id, true,
                    _typeValueParser.Parser + 
                    (
                        Lp.End 
                    )
                );
        }

        public Subject Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            var quotedTextNode = _nodeFinder.FindFirst(node, _typeValueParser.Id);
            var type = _typeValueParser.Parse(quotedTextNode);
            return new RootDefinitionSubject(type);
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public void Validate(SequencePart before, Subject subject, int subjectIndex, SequencePart after)
        {
            if (subjectIndex == 0 || before == null)
            {
                throw new ScriptParserException("A root definition subject can not be used as first subject.");
            }
            if (!(before is AssignOperator))
            {
                throw new ScriptParserException("Root definition subjects can only be used with the assignment operator.");
            }
            if (after != null)
            {
                throw new ScriptParserException("Root definition subjects can only be used as the last subject in a sequence.");
            }
        }

        public bool CanValidate(Subject subject)
        {
            return subject is RootDefinitionSubject;
        }
    }
}
