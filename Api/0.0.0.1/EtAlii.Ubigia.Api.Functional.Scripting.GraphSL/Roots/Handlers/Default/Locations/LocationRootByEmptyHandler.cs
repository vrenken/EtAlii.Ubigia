namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;

    internal class LocationRootByEmptyHandler : IRootHandler
    {

        public PathSubjectPart[] Template { get; }

        public LocationRootByEmptyHandler()
        {
            Template = new PathSubjectPart[0];
        }

        public void Process(IScriptProcessingContext context, PathSubjectPart[] match, PathSubjectPart[] rest, ExecutionScope scope, IObserver<object> output)
        {
            var hasMatch = match.Any();
            var hasRest = rest.Any();
            var parts = new PathSubjectPart[] { new ParentPathSubjectPart(), new ConstantPathSubjectPart("Location")}.AsQueryable();

            // We only add the isparentof separator when no match or rest is available.
            if (hasMatch || hasRest)
            {
                parts = parts.Concat(new[] {new ParentPathSubjectPart()});
            }
            parts = parts
                .Concat(match)
                .Concat(rest);
            var path = new AbsolutePathSubject(parts.ToArray());
            context.PathSubjectForOutputConverter.Convert(path, scope, output);
        }
    }
}