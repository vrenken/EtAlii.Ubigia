namespace EtAlii.Ubigia.Api.Transport
{
	using System;
	using System.Threading.Tasks;
    using EtAlii.xTechnology.MicroContainer;

    public interface ISpaceTransport
    {
        bool IsConnected { get; }

        void Initialize(ISpaceConnection spaceConnection, Uri address);

        Task Start(ISpaceConnection spaceConnection, Uri address);
        Task Stop(ISpaceConnection spaceConnection);
        
        IScaffolding[] CreateScaffolding();
    }
}
