namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;

    public interface IHostTestContext<out TInfrastructureTestHost> : IHostTestContext
        where TInfrastructureTestHost : class
    {
        TInfrastructureTestHost Host { get; }
    }

    public interface IHostTestContext : EtAlii.xTechnology.Hosting.IHostTestContext
    {
        string SystemAccountName { get; }
        string SystemAccountPassword { get; }
        string TestAccountName { get; }
        string TestAccountPassword { get; }
        string AdminAccountName { get; }
        string AdminAccountPassword { get; }

        Uri ManagementApiAddress { get; }
        Uri DataApiAddress { get; }

        string HostName { get; }
        
        Task<ISystemConnection> CreateSystemConnection();

        Task AddUserAccountAndSpaces(ISystemConnection connection, string accountName, string password, string[] spaceNames);
    }
}