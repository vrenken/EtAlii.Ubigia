namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Tests;
    

    internal static class TestFunctionHandlerFactoryExtension
    {
        public static IFunctionHandler[] CreateForTesting(this FunctionHandlerFactory factory)
        {
            return new IFunctionHandler[]
            {
                new TestFunctionHandler(), 
            };
        }
    }
}