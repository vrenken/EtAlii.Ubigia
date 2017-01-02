namespace EtAlii.Servus.Api.Data
{
    using System.Linq;

    internal class ItemsOutputHandler : ActionHandlerBase<ItemsOutput>
    {
        private readonly IPathHelper _pathHelper;
        private readonly ScriptScope _scope;

        public ItemsOutputHandler(
            IPathHelper pathHelper, 
            ScriptScope scope)
        {
            _pathHelper = pathHelper;
            _scope = scope;
        }

        public override void Handle(ItemsOutput action)
        {
            var nodes = _pathHelper.GetChildren(action.Path);
            if (nodes.Any())
            {
                _scope.Output(nodes);
            }
        }
    }
}
