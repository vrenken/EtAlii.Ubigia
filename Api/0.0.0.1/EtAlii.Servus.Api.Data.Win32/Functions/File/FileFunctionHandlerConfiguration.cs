namespace EtAlii.Servus.Api
{
    using System;
    using EtAlii.Servus.Api.Functional;

    public class FileFunctionHandlerConfiguration : FunctionHandlerConfiguration
    {
        public FileFunctionHandlerConfiguration(
            IFunctionHandler functionHandler,
            ArgumentSet[] argumentSets)
            : base("File", functionHandler, argumentSets) 
        {
        }
    }
}
