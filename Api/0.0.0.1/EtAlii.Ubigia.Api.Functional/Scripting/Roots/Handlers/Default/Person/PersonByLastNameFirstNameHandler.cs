namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;

    internal class PersonByLastNameFirstNameHandler : IRootHandler
    {

        public PathSubjectPart[] Template { get; }

        public PersonByLastNameFirstNameHandler()
        {
            Template = new PathSubjectPart[]
            {
                new TypedPathSubjectPart(TypedPathFormatter.Name.LastNameFormatter), 
                new ParentPathSubjectPart(),
                new TypedPathSubjectPart(TypedPathFormatter.Name.FirstNameFormatter)
            };
        }

        public void Process(IRootContext context, PathSubjectPart[] match, PathSubjectPart[] rest, ExecutionScope scope, IObserver<object> output)
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
            context.Converter.Convert(path, scope, output);
        }
    }
}