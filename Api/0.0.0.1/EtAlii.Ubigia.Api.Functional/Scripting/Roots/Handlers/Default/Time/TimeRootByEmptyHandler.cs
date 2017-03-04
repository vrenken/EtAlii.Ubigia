namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;

    internal class TimeRootByEmptyHandler : IRootHandler
    {

        public PathSubjectPart[] Template => _template;
        private readonly PathSubjectPart[] _template;

        public TimeRootByEmptyHandler()
        {
            _template = new PathSubjectPart[0];
        }

        public void Process(IRootContext context, PathSubjectPart[] match, PathSubjectPart[] rest, ExecutionScope scope, IObserver<object> output)
        {
            var hasMatch = match.Any();
            var hasRest = rest.Any();
            var parts = new PathSubjectPart[] { new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("Time")}.AsQueryable();

            // We only add the isparentof separator when no match or rest is available.
            if (hasMatch || hasRest)
            {
                parts = parts.Concat(new[] {new IsParentOfPathSubjectPart()});
            }
            parts = parts
                .Concat(match)
                .Concat(rest);
            var path = new AbsolutePathSubject(parts.ToArray());
            context.Converter.Convert(path, scope, output);
        }
    }
}