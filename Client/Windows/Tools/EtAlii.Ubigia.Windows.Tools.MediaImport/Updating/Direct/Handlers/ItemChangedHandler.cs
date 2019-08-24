namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System.Threading.Tasks;

    //using EtAlii.Ubigia.Api.Functional.Scripting

    internal class ItemChangedHandler : IItemChangedHandler
    {
        //private readonly ILogger _logger
        //private readonly IGraphSLScriptContext _scriptContext

        public ItemChangedHandler(
            //IGraphSLScriptContext scriptContext
            //ILogger logger
            )
        {
            //_logger = logger
            //_scriptContext = scriptContext
        }

        public Task Handle(ItemCheckAction action, string localStart, string remoteStart)
        {
            // Handle an item check/uncheck action.
            return Task.CompletedTask;
        }
    }
}
