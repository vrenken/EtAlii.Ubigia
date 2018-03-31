namespace EtAlii.Ubigia.Api.Transport
{
	using System;
	using System.Threading.Tasks;
    using EtAlii.xTechnology.MicroContainer;

    public interface IStorageTransport
    {
        bool IsConnected { get; }

        void Initialize(IStorageConnection storageConnection, Uri address);

        Task Start(IStorageConnection storageConnection, Uri address);

        Task Stop(IStorageConnection storageConnection);
        
        IScaffolding[] CreateScaffolding();
    }
}
