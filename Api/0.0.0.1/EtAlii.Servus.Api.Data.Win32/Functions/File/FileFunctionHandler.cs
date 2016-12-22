namespace EtAlii.Servus.Api
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Functional;

    public class FileFunctionHandler : IFunctionHandler
    {
        public ParameterSet[] ParameterSets { get { return _parameterSets; } }
        private readonly ParameterSet[] _parameterSets;

        public string Name { get { return _name; } }
        private readonly string _name;

        public FileFunctionHandler()
        {
            _parameterSets = new ParameterSet[] {};
            _name = "File";
        }

        public void Process(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject)
        {
            throw new NotImplementedException();
        }
    }
}
