namespace EtAlii.Servus.Api.Data
{
    public class RemoveItem : PathAction
    {
        public string Name { get; private set; }

        public RemoveItem(Path path, string name)
            : base(path)
        {
            Name = name;
        }

        internal override void Handle(IHandlerFactory factory)
        {
            Handle<RemoveItemHandler>(factory);
        }
    }
}
