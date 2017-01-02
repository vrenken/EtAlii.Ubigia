namespace EtAlii.Ubigia.Api.Functional.Tests
{
    internal static class TestRootHandlerFactoryExtension
    {
        public static IRootHandlerMapper[] CreateForTesting(this RootHandlerMapperFactory factory)
        {
            return new IRootHandlerMapper[]
            {
                new TestRootHandlerMapper(), 
            };
        }
    }
}