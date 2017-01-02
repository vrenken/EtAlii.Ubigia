namespace EtAlii.Servus.Api.Functional
{
    using System;

    public class FunctionHandler : IFunctionHandler
    {
        public ParameterSet[] ParameterSets { get { return _parameterSets; } }
        private readonly ParameterSet[] _parameterSets;

        public string Name { get { return _name; } }
        private readonly string _name;

        public FunctionHandler(string name,
            ParameterSet[] parameterSets)
        {
            _parameterSets = parameterSets;
            _name = name;
        }
         
        public object Process(ParameterSet parameterSet, ArgumentSet argumentSet, object input)
        {
            throw new NotImplementedException();
        }
    }
}