// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Threading.Tasks;

    internal interface IRootHandlerPathMatcher
    {
        Task<MatchResult> Match(ExecutionScope scope, IRootHandler rootHandler, PathSubjectPart[] path);
    }
}
