namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;

    internal class PersonByLastNameHandler : IRootHandler
    {

        public PathSubjectPart[] Template { get; }

        public PersonByLastNameHandler()
        {
            Template = new PathSubjectPart[] { new TypedPathSubjectPart(TypedPathFormatter.Name.LastNameFormatter) };
        }

        public void Process(IRootContext context, PathSubjectPart[] match, PathSubjectPart[] rest, ExecutionScope scope, IObserver<object> output)
        {
            var parts = new PathSubjectPart[] { new ParentPathSubjectPart(), new ConstantPathSubjectPart("Person"), new ParentPathSubjectPart() }
               .Concat(match)
               .Concat(rest)
               .ToArray();
            var path = new AbsolutePathSubject(parts);
            context.Converter.Convert(path, scope, output);
        }
    }
}