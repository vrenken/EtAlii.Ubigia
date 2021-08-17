// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal class MatchParameters
    {
        public ExecutionScope Scope { get; }
        public PathSubjectPart[] PathRest { get; }
        public IRootHandler RootHandler { get; }
        public PathSubjectPart CurrentTemplatePart { get; }

        public MatchParameters(
            IRootHandler rootHandler,
            PathSubjectPart currentTemplatePart,
            PathSubjectPart[] pathRest,
            ExecutionScope scope)
        {
            RootHandler = rootHandler;
            CurrentTemplatePart = currentTemplatePart;
            PathRest = pathRest;
            Scope = scope;
        }
    }
}
