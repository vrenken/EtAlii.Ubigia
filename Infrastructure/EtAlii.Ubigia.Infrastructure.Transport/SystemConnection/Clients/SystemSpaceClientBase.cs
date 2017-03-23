namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;

    internal abstract class SystemSpaceClientBase
    {
        protected ISpaceConnection Connection { get; private set; }

        public virtual async Task Connect(ISpaceConnection spaceConnection)
        {
            await Task.Run(() => Connection = spaceConnection);
        }

        public virtual async Task Disconnect(ISpaceConnection spaceConnection)
        {
            await Task.Run(() => Connection = null);
        }
    }
}
