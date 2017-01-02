namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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