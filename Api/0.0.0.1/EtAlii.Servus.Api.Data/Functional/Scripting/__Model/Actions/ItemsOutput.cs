namespace EtAlii.Servus.Api.Data
{
    public class ItemsOutput : PathAction
    {
        public ItemsOutput(Path path)
            : base(path)
        {
        }

        internal override void Handle(IHandlerFactory factory)
        {
            Handle<ItemsOutputHandler>(factory);
        }
    }
}
