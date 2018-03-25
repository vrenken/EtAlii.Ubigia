namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Transport;

    public interface IHostTestContext
    {
	    InfrastructureTestHost Host { get; }
	    string SystemAccountName { get; }
	    string SystemAccountPassword { get; }
	    string TestAccountName { get; }
	    string TestAccountPassword { get; }

		void Start();

        //void Start(IHost host, IInfrastructure infrastructure);
        void Stop();

        Task<ISystemConnection> CreateSystemConnection();

        Task AddUserAccountAndSpaces(ISystemConnection connection, string accountName, string password, string[] spaceNames);
    }
}