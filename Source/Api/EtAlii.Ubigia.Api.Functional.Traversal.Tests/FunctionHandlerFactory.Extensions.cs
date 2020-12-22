namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
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
