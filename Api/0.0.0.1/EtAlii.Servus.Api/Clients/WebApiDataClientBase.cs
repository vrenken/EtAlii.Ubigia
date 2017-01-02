using EtAlii.xTechnology.MicroContainer;
using System;
namespace EtAlii.Servus.Api
{

    public abstract class WebApiDataClientBase<C> : IDataClient
    {
        protected IInfrastructureClient Client { get { return _client; } }
        private readonly IInfrastructureClient _client;

        protected IAddressFactory AddressFactory { get { return _addressFactory; } }
        private readonly IAddressFactory _addressFactory;

        protected C Connection { get { return _connection.Value; } }
        private readonly Lazy<C> _connection;

        protected WebApiDataClientBase(Container container, IAddressFactory addressFactory, IInfrastructureClient client)
            : base()
        {
            _connection = new Lazy<C>(() => container.GetInstance<C>());
            _addressFactory = addressFactory;
            _client = client;
        }

        public virtual void Connect()
        {
        }

        public virtual void Disconnect()
        {
        }
    }
}
