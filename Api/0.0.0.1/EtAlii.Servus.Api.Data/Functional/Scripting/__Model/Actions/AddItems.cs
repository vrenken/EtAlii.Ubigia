namespace EtAlii.Servus.Api.Data
{
    public class AddItems : PathAction
    {
        public string Name { get; private set; }

        public AddItems(Path path, string name)
            : base(path)
        {
            Name = name;
        }

        internal override void Handle(IHandlerFactory factory)
        {
            Handle<AddItemsHandler>(factory);
        }
    }
}
