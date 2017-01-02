namespace EtAlii.Servus.Api
{
    using System;
    using EtAlii.Servus.Api.Functional;

    public class RenameFunctionHandlerConfiguration : FunctionHandlerConfiguration
    {
        public RenameFunctionHandlerConfiguration(
            IFunctionHandler functionHandler,
            ArgumentSet[] argumentSets)
            : base("Rename", functionHandler, argumentSets) 
        {
        }
    }
}
