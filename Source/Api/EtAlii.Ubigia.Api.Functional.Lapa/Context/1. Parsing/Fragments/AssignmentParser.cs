// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Moppet.Lapa;

    internal class AssignmentParser : IAssignmentParser
    {
        public LpsParser Parser { get; }

        public AssignmentParser(IWhitespaceParser whitespaceParser)
        {
            Parser = whitespaceParser.Optional + Lp.Char('<') + Lp.Char('=') + whitespaceParser.Optional;
        }
    }
}
