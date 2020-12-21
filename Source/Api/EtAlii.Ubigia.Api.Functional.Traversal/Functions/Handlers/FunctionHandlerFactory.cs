namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal class FunctionHandlerFactory : IFunctionHandlerFactory
    {
        public IFunctionHandler[] CreateDefaults()
        {
            return new IFunctionHandler[]
            {
                new IdFunctionHandler(),
                new RenameFunctionHandler(),
                new CountFunctionHandler(),
                new NewFunctionHandler(),
                new IncludeFunctionHandler(),
            };
        }
    }
}
