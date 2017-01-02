namespace EtAlii.Servus.Api.Data
{
    internal class AddItemHandler : ActionHandlerBase<AddItem>
    {
        private readonly IAddItemHelper _addItemHelper;
        private readonly IPathHelper _pathHelper;
        private readonly ScriptScope _scope;

        public AddItemHandler(
            IAddItemHelper addItemHelper,
            IPathHelper pathHelper,
            ScriptScope scope)
        {
            _addItemHelper = addItemHelper;
            _pathHelper = pathHelper;
            _scope = scope;
        }

        public override void Handle(AddItem action)
        {
            var entry = _pathHelper.GetEntry(action.Path);
            var node = _addItemHelper.AddNewEntry(action, entry);
            _scope.Output(node);
        }
    }
}
