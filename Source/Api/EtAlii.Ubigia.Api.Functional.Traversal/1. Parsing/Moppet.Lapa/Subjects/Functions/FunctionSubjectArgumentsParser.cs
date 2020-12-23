namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;
    using Moppet.Lapa;

    internal class FunctionSubjectArgumentsParser : IFunctionSubjectArgumentsParser
    {
        public string Id { get; } = nameof(FunctionSubjectArgument);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly IFunctionSubjectArgumentParser[] _parsers;

        public FunctionSubjectArgumentsParser(
            IConstantFunctionSubjectArgumentParser constantFunctionSubjectArgumentParser,
            IVariableFunctionSubjectArgumentParser variableFunctionSubjectArgumentParser,
            INonRootedPathFunctionSubjectArgumentParser nonRootedPathFunctionSubjectArgumentParser,
            IRootedPathFunctionSubjectArgumentParser rootedPathFunctionSubjectArgumentParser,
            INodeValidator nodeValidator)
        {
            _parsers = new IFunctionSubjectArgumentParser[]
            {
                constantFunctionSubjectArgumentParser,
                variableFunctionSubjectArgumentParser,
                rootedPathFunctionSubjectArgumentParser,
                nonRootedPathFunctionSubjectArgumentParser
            };
            _nodeValidator = nodeValidator;
            var lpsParsers = _parsers.Aggregate(new LpsAlternatives(), (current, parser) => current | (parser.Parser));

            Parser = new LpsParser(Id, true, lpsParsers);
        }

        public FunctionSubjectArgument Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            var childNode = node.Children.Single();
            var parser = _parsers.Single(p => p.CanParse(childNode));
            var result = parser.Parse(childNode);
            return result;
        }

        public void Validate(FunctionSubjectArgument before, FunctionSubjectArgument argument, int parameterIndex, FunctionSubjectArgument after)
        {
            var parser = _parsers.Single(p => p.CanValidate(argument));
            parser.Validate(before, argument, parameterIndex, after);
        }
    }
}
