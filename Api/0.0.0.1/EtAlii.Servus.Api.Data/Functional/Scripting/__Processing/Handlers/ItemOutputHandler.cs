namespace EtAlii.Servus.Api.Data
{
    internal class ItemOutputHandler : ActionHandlerBase<ItemOutput>
    {
        private readonly IPathHelper _pathHelper;
        private readonly ScriptScope _scope;

        public ItemOutputHandler(
            IPathHelper pathHelper, 
            ScriptScope scope)
        {
            _pathHelper = pathHelper;
            _scope = scope;
        }

        public override void Handle(ItemOutput action)
        {
            var node = _pathHelper.Get(action.Path);
            if (node != null)
            {
                _scope.Output(node);
            }
        }
    }
}
