namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Linq;

    class ConstantRootHandlerPathPartMatcher : IConstantRootHandlerPathPartMatcher
    {
        public MatchResult[] Match(MatchParameters parameters)
        {
            var match = parameters.PathRest.Take(1).ToArray();
            var rest = parameters.PathRest.Skip(1).ToArray();
            return new MatchResult[] { new MatchResult(null, match, rest) };
        }

        public bool CanMatch(MatchParameters parameters)
        {
            bool canMatch = false;

            var next = parameters.PathRest.FirstOrDefault();
            if (next != null)
            {
                var name = (next as ConstantPathSubjectPart)?.Name;
                var requiredName = ((ConstantPathSubjectPart) parameters.CurrentTemplatePart).Name;

                if (String.Equals(requiredName, name, StringComparison.OrdinalIgnoreCase))
                {
                    canMatch = true;
                }
            }
            return canMatch;
        }
    }
}