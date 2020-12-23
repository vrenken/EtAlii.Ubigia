// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal class ConstantPathSubjectPartParser : IConstantPathSubjectPartParser
    {
        public string Id { get; } = nameof(ConstantPathSubjectPart);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private const string _textId = "Text";

        public ConstantPathSubjectPartParser(
            INodeValidator nodeValidator,
            IConstantHelper constantHelper,
            INodeFinder nodeFinder)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;

            Parser = new LpsParser(Id, true,
                (Lp.One(constantHelper.IsValidConstantCharacter).OneOrMore().Id(_textId)) |
                (Lp.One(c => c == '\"') + Lp.One(c => constantHelper.IsValidQuotedConstantCharacter(c, '\"')).ZeroOrMore().Id(_textId) + Lp.One(c => c == '\"')) |
                (Lp.One(c => c == '\'') + Lp.One(c => constantHelper.IsValidQuotedConstantCharacter(c, '\'')).ZeroOrMore().Id(_textId) + Lp.One(c => c == '\''))
            );
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public PathSubjectPart Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            var text = _nodeFinder.FindFirst(node, _textId).Match.ToString();
            return new ConstantPathSubjectPart(text);
        }

        public void Validate(PathSubjectPartParserArguments arguments)
        {
            if (arguments.Before is ConstantPathSubjectPart || arguments.After is ConstantPathSubjectPart)
            {
                throw new ScriptParserException("Two constant path parts cannot be combined.");
            }
            if (arguments.PartIndex != 0 || arguments.After == null)
            {
                var constant = (ConstantPathSubjectPart)arguments.Part;
                if (constant.Name == string.Empty)
                {
                    throw new ScriptParserException("An empty constant path part is only allowed in single part paths.");
                }
            }
            if (arguments.PartIndex == 0 && arguments.After != null)
            {
                var constant = (ConstantPathSubjectPart)arguments.Part;
                if (constant.Name == string.Empty)
                {
                    throw new ScriptParserException("An empty constant path part is only allowed in single part paths.");
                }
            }
        }

        public bool CanValidate(PathSubjectPart part)
        {
            return part is ConstantPathSubjectPart;
        }
    }
}
