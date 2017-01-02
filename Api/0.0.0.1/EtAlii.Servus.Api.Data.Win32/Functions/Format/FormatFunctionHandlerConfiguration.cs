namespace EtAlii.Servus.Api
{
    using System;
    using EtAlii.Servus.Api.Functional;

    public class FormatFunctionHandlerConfiguration : FunctionHandlerConfiguration
    {
        public FormatFunctionHandlerConfiguration(
            IFunctionHandler functionHandler,
            ArgumentSet[] argumentSets)
            : base("Format", functionHandler, argumentSets) 
        {
        }
    }
}
