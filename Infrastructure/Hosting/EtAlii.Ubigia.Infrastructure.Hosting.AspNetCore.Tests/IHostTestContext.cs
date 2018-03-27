namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using EtAlii.Ubigia.Infrastructure.Transport;

    public interface IHostTestContext
    {
	    InfrastructureTestHost Host { get; }
	    string SystemAccountName { get; }
	    string SystemAccountPassword { get; }
	    string TestAccountName { get; }
	    string TestAccountPassword { get; }

	    string HostAddress { get; }
	    string ManagementServiceAddress { get; }
	    string DataServiceAddress { get; }

		string HostName { get; }

	    IInfrastructureClient CreateRestInfrastructureClient();

		void Start();

        //void Start(IHost host, IInfrastructure infrastructure);
        void Stop();

        Task<ISystemConnection> CreateSystemConnection();

        Task AddUserAccountAndSpaces(ISystemConnection connection, string accountName, string password, string[] spaceNames);
    }
}