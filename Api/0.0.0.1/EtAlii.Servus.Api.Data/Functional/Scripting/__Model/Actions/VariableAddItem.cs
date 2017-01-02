namespace EtAlii.Servus.Api.Data
{
    public class VariableAddItem : AddItem
    {
        public string Variable { get; private set; }

        public VariableAddItem(Path path, Path itemToAdd, string variable)
            : base(path, itemToAdd)
        {
            Variable = variable;
        }

        internal override void Handle(IHandlerFactory factory)
        {
            Handle<VariableAddItemHandler>(factory);
        }
    }
}
