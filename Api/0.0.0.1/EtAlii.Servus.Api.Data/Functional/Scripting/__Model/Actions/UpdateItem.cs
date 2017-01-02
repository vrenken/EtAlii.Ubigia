namespace EtAlii.Servus.Api.Data
{
    public class UpdateItem : PathAction
    {
        public string UpdateVariable { get; private set; }

        public UpdateItem(Path path, string updateVariable)
            : base(path)
        {
            UpdateVariable = updateVariable;
        }

        internal override void Handle(IHandlerFactory factory)
        {
            Handle<UpdateItemHandler>(factory);
        }
    }
}
