namespace EtAlii.Ubigia.Api.Transport
{
	using System;
	using System.Threading.Tasks;
    using EtAlii.xTechnology.MicroContainer;

    public interface IStorageTransport
    {
        bool IsConnected { get; }

        Uri Address { get; }

        Task Start();
        Task Stop();
        
        IScaffolding[] CreateScaffolding();
    }
}
