namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Linq;

    internal class PersonByLastNameHandler : IRootHandler
    {

        public PathSubjectPart[] Template { get; }

        public PersonByLastNameHandler()
        {
            Template = new PathSubjectPart[]
            {
                new TypedPathSubjectPart(NamePathFormatter.LastNameFormatter)
            };
        }

        public void Process(IScriptProcessingContext context, PathSubjectPart[] match, PathSubjectPart[] rest, ExecutionScope scope, IObserver<object> output)
        {
            var parts = new PathSubjectPart[]
                {
                    new ParentPathSubjectPart(),
                    new ConstantPathSubjectPart("Person"),
                    new ParentPathSubjectPart()
                }
               .Concat(match)
               .Concat(rest)
               .ToArray();
            var path = new AbsolutePathSubject(parts);
            context.PathSubjectForOutputConverter.Convert(path, scope, output);
        }
    }
}
