// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal class RootSubjectParser : IRootSubjectParser
    {
        public string Id { get; } = nameof(RootSubject);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private const string _textId = "Text";

        public RootSubjectParser(
            INodeValidator nodeValidator,
            INodeFinder nodeFinder,
            IConstantHelper constantHelper)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;

            Parser = new LpsParser(Id, true, Lp.Term("root:", true) +
                (
                    (Lp.One(constantHelper.IsValidConstantCharacter).OneOrMore().Id(_textId)) |
                    (Lp.One(c => c == '\"') + Lp.One(c => constantHelper.IsValidQuotedConstantCharacter(c, '\"')).ZeroOrMore().Id(_textId) + Lp.One(c => c == '\"')) |
                    (Lp.One(c => c == '\'') + Lp.One(c => constantHelper.IsValidQuotedConstantCharacter(c, '\'')).ZeroOrMore().Id(_textId) + Lp.One(c => c == '\''))
                )
            );
        }

        public Subject Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            var name = _nodeFinder.FindFirst(node, _textId).Match.ToString();
            return new RootSubject(name);
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public void Validate(SequencePart before, Subject item, int itemIndex, SequencePart after)
        {
            if (itemIndex != 0 || before != null)
            {
                throw new ScriptParserException("A root subject can only be used as first subject.");
            }
            if (!(after is AssignOperator))
            {
                throw new ScriptParserException("Root subjects can only be modified using the assignment operator.");
            }
        }

        public bool CanValidate(Subject item)
        {
            return item is RootSubject;
        }
    }
}
