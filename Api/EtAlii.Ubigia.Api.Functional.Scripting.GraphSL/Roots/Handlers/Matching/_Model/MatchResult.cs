﻿namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    internal partial class MatchResult
    {
        public IRootHandler RootHandler { get; }
        public PathSubjectPart[] Match { get; }
        public PathSubjectPart[] Rest { get; }

        // ReSharper disable InconsistentNaming
        public static readonly MatchResult NoMatch = new MatchResult(null, new PathSubjectPart[0], new PathSubjectPart[0]);

        public MatchResult(IRootHandler rootHandler, PathSubjectPart[] match, PathSubjectPart[] rest)
        {
            RootHandler = rootHandler;
            Match = match;
            Rest = rest;
        }
    }
}