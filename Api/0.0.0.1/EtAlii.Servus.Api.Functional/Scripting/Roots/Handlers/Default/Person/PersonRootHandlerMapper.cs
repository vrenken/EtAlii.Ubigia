namespace EtAlii.Servus.Api.Functional
{
    using System;

    internal class PersonRootHandlerMapper : IRootHandlerMapper
    {
        public string Name { get { return _name; } }
        private readonly string _name;

        public IRootHandler[] AllowedPaths { get { return _allowedPaths; } }
        private readonly IRootHandler[] _allowedPaths;

        public PersonRootHandlerMapper()
        {
            _name = "person";

            _allowedPaths = new IRootHandler[]
            {
                new PersonByLastNameFirstNameHandler() 
            };
        }

        public void Process(IRootContext context, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject)
        {
            //context.PathProcessor.Process()
            throw new NotImplementedException();
        }
    }
}