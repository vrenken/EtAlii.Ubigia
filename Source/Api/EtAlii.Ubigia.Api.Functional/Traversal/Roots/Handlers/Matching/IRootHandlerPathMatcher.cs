// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Threading.Tasks;

    internal interface IRootHandlerPathMatcher
    {
        Task<MatchResult> Match(IScriptScope scope, IRootHandler rootHandler, PathSubjectPart[] path);
    }
}
