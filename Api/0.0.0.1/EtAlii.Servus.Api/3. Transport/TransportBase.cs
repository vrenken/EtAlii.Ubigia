namespace EtAlii.Servus.Api.Transport
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.MicroContainer;

    public abstract class TransportBase : ITransport
    {
        public IClientContext Clients { get { return _clients; } }
        private IClientContext _clients;

        public bool IsConnected { get { return _isConnected; } }
        private bool _isConnected;

        public virtual async Task Open(string address)
        {
            await _clients.Open(this);
            _isConnected = true;
        }

        public virtual async Task Close()
        {
            await _clients.Close(this);
            _isConnected = false;
        }

        protected abstract IScaffolding[] CreateScaffoldingInternal();

        IScaffolding[] ITransport.CreateScaffolding()
        {
            return CreateScaffoldingInternal();
        }

        protected abstract void InvokeInternal(Action<ITransport> invocation);

        void ITransport.Invoke(Action<ITransport> invocation)
        {
            InvokeInternal(invocation);
        }

        void ITransport.Initialize(IClientContext clients)
        {
            _clients = clients;
        }
    }
}
