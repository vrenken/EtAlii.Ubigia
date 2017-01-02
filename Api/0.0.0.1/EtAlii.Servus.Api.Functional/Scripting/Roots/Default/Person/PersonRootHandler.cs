namespace EtAlii.Servus.Api.Functional
{
    using System;

    internal class PersonRootHandler : IRootHandler
    {
        public string Name { get { return _name; } }
        private readonly string _name;

        public IRootSubHandler[] AllowedPaths { get { return _allowedPaths; } }
        private readonly IRootSubHandler[] _allowedPaths;

        public PersonRootHandler()
        {
            _name = "person";

            _allowedPaths = new IRootSubHandler[]
            {
            };
        }

        public void Process(IRootContext context, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject)
        {
            //context.PathProcessor.Process()
            throw new NotImplementedException();
        }
    }
}