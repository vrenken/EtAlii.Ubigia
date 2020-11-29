namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System;

    internal partial class MatchResult
    {
        public IRootHandler RootHandler { get; }
        public PathSubjectPart[] Match { get; }
        public PathSubjectPart[] Rest { get; }

        public static readonly MatchResult NoMatch = new MatchResult(null, Array.Empty<PathSubjectPart>(), Array.Empty<PathSubjectPart>());

        public MatchResult(IRootHandler rootHandler, PathSubjectPart[] match, PathSubjectPart[] rest)
        {
            RootHandler = rootHandler;
            Match = match;
            Rest = rest;
        }
    }
}