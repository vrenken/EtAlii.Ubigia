namespace EtAlii.Servus.Api.Functional
{
    internal class FunctionHandlersProvider : IFunctionHandlersProvider
    {
        public IFunctionHandler[] FunctionHandlers { get { return _functionHandlers; } }
        private readonly IFunctionHandler[] _functionHandlers;

        public static readonly IFunctionHandlersProvider Empty = new FunctionHandlersProvider(new IFunctionHandler[] {});

        public FunctionHandlersProvider(IFunctionHandler[] functionHandlers)
        {
            _functionHandlers = functionHandlers;
        }
    }
}