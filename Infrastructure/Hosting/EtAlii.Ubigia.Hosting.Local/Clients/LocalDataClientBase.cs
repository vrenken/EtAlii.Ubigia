namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using EtAlii.xTechnology.MicroContainer;
    using System;

    public abstract class LocalDataClientBase<C> : IDataClient
    {
        protected C Connection { get { return _connection.Value; } }
        private readonly Lazy<C> _connection;

        protected LocalDataClientBase(Container container)
            : base()
        {
            _connection = new Lazy<C>(() => container.GetInstance<C>());
        }

        public virtual void Connect()
        {
        }

        public virtual void Disconnect()
        {
        }
    }
}
