﻿namespace EtAlii.Ubigia.Api.Functional
{
    internal class PathSubjectPartParserArguments
    {
        public Subject Subject { get; }
        public PathSubjectPart Before { get; }
        public PathSubjectPart Part { get; }
        public int PartIndex { get; }
        public PathSubjectPart After { get; }

        public PathSubjectPartParserArguments(Subject subject, PathSubjectPart before, PathSubjectPart part, int partIndex, PathSubjectPart after)
        {
            Subject = subject;
            Before = before;
            Part = part;
            PartIndex = partIndex;
            After = after;
        }
    }
}
