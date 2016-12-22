namespace EtAlii.Servus.Api.Functional
{
    using System;

    public class PersonByLastNameFirstNameHandler : IRootHandler
    {

        public PathSubjectPart[] Template { get { return _template; } }
        private readonly PathSubjectPart[] _template;

        public PersonByLastNameFirstNameHandler()
        {
            _template = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("lastName"), new IsParentOfPathSubjectPart(),
                new ConstantPathSubjectPart("firstName")
            };
        }

        public void Process(IRootContext context, IObservable<object> input, ExecutionScope scope,
            IObserver<object> output, bool processAsSubject)
        {
            
        }
    }
}