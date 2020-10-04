﻿namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using Moppet.Lapa;

    internal class ConstantFunctionSubjectArgumentParser : IConstantFunctionSubjectArgumentParser
    {
        public string Id { get; } = nameof(ConstantFunctionSubjectArgument);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private const string TextId = "Text";

        public ConstantFunctionSubjectArgumentParser(
            INodeValidator nodeValidator,
            IConstantHelper constantHelper,
            INodeFinder nodeFinder)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;

            Parser = new LpsParser(Id, true,
                //(Lp.One(c => _constantHelper.IsValidConstantCharacter(c)).OneOrMore().Id(TextId)) |
                (Lp.One(c => c == '\"') +
                 Lp.One(c => constantHelper.IsValidQuotedConstantCharacter(c, '\"')).OneOrMore().Id(TextId) +
                 Lp.One(c => c == '\"')) |
                (Lp.One(c => c == '\'') +
                 Lp.One(c => constantHelper.IsValidQuotedConstantCharacter(c, '\'')).OneOrMore().Id(TextId) +
                 Lp.One(c => c == '\''))
                );
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public FunctionSubjectArgument Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            var text = _nodeFinder.FindFirst(node, TextId).Match.ToString();
            return new ConstantFunctionSubjectArgument(text);
        }

        public void Validate(FunctionSubjectArgument before, FunctionSubjectArgument argument, int parameterIndex, FunctionSubjectArgument after)
        {
            // Make sure the argument can can actually be applied on the before/after FunctionSubjectArgument combination.
        }

        public bool CanValidate(FunctionSubjectArgument argument)
        {
            return argument is ConstantFunctionSubjectArgument;
        }
    }
}