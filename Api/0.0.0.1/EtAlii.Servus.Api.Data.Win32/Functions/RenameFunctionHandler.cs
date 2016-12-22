namespace EtAlii.Servus.Api
{
    using System;
    using EtAlii.Servus.Api.Functional;
    using EtAlii.Servus.Api.Logical;

    public class RenameFunctionHandler : IFunctionHandler
    {
        public string Name { get { return _name; } }
        private const string _name = "Rename";

        public Type[] Parameters { get { return _parameters; } }
        private readonly Type[] _parameters;

        public RenameFunctionHandler()
        {
            
        }

        public void Process()
        {
            throw new NotImplementedException();
        }
    }
}
