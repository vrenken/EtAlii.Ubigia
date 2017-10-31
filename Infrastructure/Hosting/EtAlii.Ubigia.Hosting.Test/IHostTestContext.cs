namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport;
    using xTechnology.Hosting;

    public interface IHostTestContext
    {
        TestHost Host { get; }
        string SystemAccountName { get; }
        string SystemAccountPassword { get; }

        void Start();

        void Start(IHost host, IInfrastructure infrastructure);
        void Stop();

        Task<ISystemConnection> CreateSystemConnection();

        Task AddUserAccountAndSpaces(ISystemConnection connection, string accountName, string password, string[] spaceNames);
    }
}