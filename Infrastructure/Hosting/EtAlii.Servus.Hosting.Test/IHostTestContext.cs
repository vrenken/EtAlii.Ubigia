namespace EtAlii.Servus.Infrastructure.Hosting.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Servus.Infrastructure;
    using EtAlii.Servus.Infrastructure.Transport;

    public interface IHostTestContext
    {
        TestHost Host { get; }
        string SystemAccountName { get; }
        string SystemAccountPassword { get; }

        void Start();

        void Start(IHost host);
        void Stop();

        Task<ISystemConnection> CreateSystemConnection();

        Task AddUserAccountAndSpaces(ISystemConnection connection, string accountName, string password, string[] spaceNames);
    }
}