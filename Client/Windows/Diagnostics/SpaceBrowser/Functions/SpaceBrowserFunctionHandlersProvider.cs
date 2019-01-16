namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
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
