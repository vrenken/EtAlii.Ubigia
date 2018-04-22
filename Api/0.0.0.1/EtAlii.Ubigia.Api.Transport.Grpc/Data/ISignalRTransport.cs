namespace EtAlii.Ubigia.Api.Transport.SignalR
{
	using System.Net.Http;

	public interface ISignalRTransport
    {
	    HttpMessageHandler HttpMessageHandler { get; }

		string AuthenticationToken { get; set; }
    }
}