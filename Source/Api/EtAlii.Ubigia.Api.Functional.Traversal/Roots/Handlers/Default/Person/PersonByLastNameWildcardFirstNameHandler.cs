namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Linq;

    internal class PersonByLastNameWildcardFirstNameHandler : IRootHandler
    {

        public PathSubjectPart[] Template { get; }

        public PersonByLastNameWildcardFirstNameHandler()
        {
            Template = new PathSubjectPart[]
            {
                new WildcardPathSubjectPart("*"), new ParentPathSubjectPart(),
                new TypedPathSubjectPart(NamePathFormatter.LastNameFormatter),
            };
        }

        public void Process(IScriptProcessingContext context, PathSubjectPart[] match, PathSubjectPart[] rest, ExecutionScope scope, IObserver<object> output)
        {
            var parts = new PathSubjectPart[] { new ParentPathSubjectPart(), new ConstantPathSubjectPart("Person"), new ParentPathSubjectPart() }
               .Concat(match)
               .Concat(rest)
               .ToArray();
            var path = new AbsolutePathSubject(parts);
            context.PathSubjectForOutputConverter.Convert(path, scope, output);
        }
    }
}
