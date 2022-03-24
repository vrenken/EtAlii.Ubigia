// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal sealed class WhitespaceParser : IWhitespaceParser
    {
        public LpsParser Required { get; }

        public LpsParser Optional { get; }

        public WhitespaceParser()
        {
            Required = Lp.OneOrMore(c => c == ' ' || c == '\t' || c == '\r');
            Optional = Lp.ZeroOrMore(c => c == ' ' || c == '\t' || c == '\r');
        }
    }
}
