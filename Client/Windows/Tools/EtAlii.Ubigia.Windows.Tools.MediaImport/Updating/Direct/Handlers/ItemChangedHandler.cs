namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.xTechnology.Logging;

    internal class ItemChangedHandler : IItemChangedHandler
    {
        private readonly ILogger _logger;
        private readonly IGraphSLScriptContext _scriptContext;

        public ItemChangedHandler(
            IGraphSLScriptContext scriptContext,
            ILogger logger)
        {
            _logger = logger;
            _scriptContext = scriptContext;
        }

        public void Handle(ItemCheckAction action, string localStart, string remoteStart)
        {
        }

    }
}
