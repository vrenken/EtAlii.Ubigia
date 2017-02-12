namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api.Functional;

    public class SpaceBrowserFunctionHandlersProvider : ISpaceBrowserFunctionHandlersProvider
    { 
        public IFunctionHandler[] FunctionHandlers { get; }

        public SpaceBrowserFunctionHandlersProvider(IViewFunctionHandler viewFunctionHandler)
        {
            FunctionHandlers = new IFunctionHandler[]
            {
                viewFunctionHandler
            };
        }
    }
}
