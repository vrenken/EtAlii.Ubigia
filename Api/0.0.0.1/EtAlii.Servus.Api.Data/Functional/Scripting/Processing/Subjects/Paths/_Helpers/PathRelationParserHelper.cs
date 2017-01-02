﻿namespace EtAlii.Servus.Api.Data
{
    using System;
    using Moppet.Lapa;
    using System.Collections.Generic;

    internal class PathRelationParserHelper : IPathRelationParserHelper
    {

        public readonly LpsParser LeftSource;
        public readonly LpsParser RightTarget;

        public readonly LpsParser LeftTarget;
        public readonly LpsParser RightSource;

        public PathRelationParserHelper()
        {
            LeftSource = Lp.Term("-");
            RightTarget = Lp.Term("->");

            LeftTarget = Lp.Term("<-");
            RightSource = Lp.Term("-");
        }

        public LpsParser CreatePathRelationParser(string name, string symbol)
        {
            var labeledName = String.Format(@"[:{0}]", name);
            var labeledSymbol = String.Format(@"[{0}]", symbol);

            return new LpsParser(symbol, true)
            {
                Parser = LeftSource + Lp.Term(labeledName) + RightTarget |
                         LeftSource + Lp.Term(labeledSymbol) + RightTarget |
                         Lp.Term(symbol)
            };
        }
    }
}
