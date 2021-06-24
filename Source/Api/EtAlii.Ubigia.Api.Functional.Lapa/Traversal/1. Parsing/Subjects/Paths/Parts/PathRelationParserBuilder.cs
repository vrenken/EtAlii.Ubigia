// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal class PathRelationParserBuilder : IPathRelationParserBuilder
    {

        private readonly LpsParser _leftSource;
        private readonly LpsParser _rightTarget;

        //private readonly LpsParser _leftTarget
        //private readonly LpsParser _rightSource

        public PathRelationParserBuilder()
        {
            _leftSource = Lp.Term("-");
            _rightTarget = Lp.Term("->");

            //_leftTarget = Lp.Term("<-")
            //_rightSource = Lp.Term("-")
        }

        public LpsParser CreatePathRelationParser(string name, string symbol)
        {
            var labeledName = $@"[:{name}]";
            var labeledSymbol = $@"[{symbol}]";

            return new LpsParser(symbol, true)
            {
                Parser = _leftSource + Lp.Term(labeledName) + _rightTarget |
                         _leftSource + Lp.Term(labeledSymbol) + _rightTarget |
                         Lp.Term(symbol)
            };
        }
    }
}
