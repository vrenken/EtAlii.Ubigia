namespace EtAlii.Servus.Api
{
    using System;
    using EtAlii.Servus.Api.Functional;

    public class RenameFunctionHandler : IFunctionHandler
    {
        public ParameterSet[] ParameterSets { get { return _parameterSets; } }
        private readonly ParameterSet[] _parameterSets;

        public string Name { get { return _name; } }
        private readonly string _name;

        public RenameFunctionHandler()
        {
            _parameterSets = new ParameterSet[] {};
            _name = "Rename";
        }

        public object Process(FunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, object input)
        {
            throw new NotImplementedException();
        }
    }
}
