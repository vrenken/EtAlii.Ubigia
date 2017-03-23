namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;

    class ConstantRootHandlerPathPartMatcher : IConstantRootHandlerPathPartMatcher
    {
        private readonly IPathSubjectPartContentGetter _pathSubjectPartContentGetter;

        public ConstantRootHandlerPathPartMatcher(IPathSubjectPartContentGetter pathSubjectPartContentGetter)
        {
            _pathSubjectPartContentGetter = pathSubjectPartContentGetter;
        }

        public MatchResult[] Match(MatchParameters parameters)
        {
            var match = parameters.PathRest.Take(1).ToArray();
            var rest = parameters.PathRest.Skip(1).ToArray();
            return new[] { new MatchResult(null, match, rest) };
        }

        public bool CanMatch(MatchParameters parameters)
        {
            bool canMatch = false;

            var next = parameters.PathRest.FirstOrDefault();
            if (next != null)
            {
                var content = _pathSubjectPartContentGetter.GetPartContent(next, parameters.Scope);
                if (content != null)
                {
                    var requiredName = ((ConstantPathSubjectPart) parameters.CurrentTemplatePart).Name;

                    if (String.Equals(requiredName, content, StringComparison.OrdinalIgnoreCase))
                    {
                        canMatch = true;
                    }
                }
            }
            return canMatch;
        }
    }
}