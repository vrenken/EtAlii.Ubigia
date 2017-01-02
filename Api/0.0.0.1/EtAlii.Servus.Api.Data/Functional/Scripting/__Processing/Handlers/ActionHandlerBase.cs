namespace EtAlii.Servus.Api.Data
{
    internal abstract class ActionHandlerBase<T> : IActionHandler<T>
        where T: Action
    {
        public abstract void Handle(T action);

        void IActionHandler.Handle(Action action)
        {
            Handle((T)action);
        }

        public bool CanHandle(Action action)
        {
            return action is T;
        }
    }
}
