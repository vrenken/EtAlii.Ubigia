namespace EtAlii.Servus.Api.Data
{
    public class ItemOutput : PathAction
    {
        public ItemOutput(Path path)
            : base(path)
        {
        }

        internal override void Handle(IHandlerFactory factory)
        {
            Handle<ItemOutputHandler>(factory);
        }
    }
}
