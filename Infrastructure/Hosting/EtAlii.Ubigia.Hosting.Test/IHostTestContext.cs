namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Transport;

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