namespace EtAlii.Servus.Infrastructure.Transport
{
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Transport;

    internal abstract class SystemSpaceClientBase
    {
        protected ISpaceConnection Connection { get { return _connection; } }
        private ISpaceConnection _connection;

        public virtual async Task Connect(ISpaceConnection spaceConnection)
        {
            await Task.Run(() => _connection = spaceConnection);
        }

        public virtual async Task Disconnect(ISpaceConnection spaceConnection)
        {
            await Task.Run(() => _connection = null);
        }
    }
}
