﻿namespace EtAlii.Servus.Api.Functional
{
    using System.Linq;
    using Moppet.Lapa;

    internal class FunctionSubjectArgumentsParser : IFunctionSubjectArgumentsParser
    {
        public string Id { get { return _id; } }
        private readonly string _id = "FunctionSubjectArgument";

        public LpsParser Parser { get { return _parser; } }
        private readonly LpsParser _parser;

        private readonly INodeValidator _nodeValidator;
        private readonly IFunctionSubjectArgumentParser[] _parsers;

        public FunctionSubjectArgumentsParser(
            IConstantFunctionSubjectArgumentParser constantFunctionSubjectArgumentParser,
            IVariableFunctionSubjectArgumentParser variableFunctionSubjectArgumentParser,
            IPathFunctionSubjectArgumentParser pathFunctionSubjectArgumentParser,
            INodeValidator nodeValidator)
        {
            _parsers = new IFunctionSubjectArgumentParser[]
            {
                constantFunctionSubjectArgumentParser,
                variableFunctionSubjectArgumentParser,
                pathFunctionSubjectArgumentParser
            };
            _nodeValidator = nodeValidator;
            var lpsParsers = _parsers.Aggregate(new LpsAlternatives(), (current, parser) => current | (parser.Parser));

            _parser = new LpsParser(Id, true, lpsParsers);
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
