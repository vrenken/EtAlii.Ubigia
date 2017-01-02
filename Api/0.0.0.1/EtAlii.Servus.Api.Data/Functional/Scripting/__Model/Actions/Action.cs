namespace EtAlii.Servus.Api.Data
{
    public abstract class Action
    {
        internal abstract void Handle(IHandlerFactory factory);

        protected void Handle<T>(IHandlerFactory factory)
            where T: class, IActionHandler
        {
            var handler = factory.Create<T>();
            handler.Handle(this);
        }
    }
}
