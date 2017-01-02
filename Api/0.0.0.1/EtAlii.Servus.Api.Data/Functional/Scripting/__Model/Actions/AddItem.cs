namespace EtAlii.Servus.Api.Data
{
    public class AddItem : PathAction
    {
        public Path ItemPath { get; private set; }

        public AddItem(Path path, Path itemPath)
            : base(path)
        {
            ItemPath = itemPath;
        }

        internal override void Handle(IHandlerFactory factory)
        {
            Handle<AddItemHandler>(factory);
        }
    }
}
