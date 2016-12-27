namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Linq;

    internal class PersonByLastNameFirstNameHandler : IRootHandler
    {

        public PathSubjectPart[] Template { get { return _template; } }
        private readonly PathSubjectPart[] _template;

        public PersonByLastNameFirstNameHandler()
        {
            _template = new PathSubjectPart[]
            {
                new TypedPathSubjectPart(TypedPathFormatter.Name.LastNameFormatter), new IsParentOfPathSubjectPart(),
                new TypedPathSubjectPart(TypedPathFormatter.Name.FirstNameFormatter)
            };
        }

        public void Process(IRootContext context, PathSubjectPart[] match, PathSubjectPart[] rest, ExecutionScope scope, IObserver<object> output)
        {
            var parts = new PathSubjectPart[] { new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("Person"), new IsParentOfPathSubjectPart() }
               .Concat(match)
               .Concat(rest)
               .ToArray();
            var path = new AbsolutePathSubject(parts);
            context.Converter.Convert(path, scope, output);
        }
    }
}