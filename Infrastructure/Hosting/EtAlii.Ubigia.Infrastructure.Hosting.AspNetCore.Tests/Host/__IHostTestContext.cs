//namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
//{
//	using System;
//	using System.Threading.Tasks;
//	using EtAlii.Ubigia.Infrastructure.Functional;
//	using EtAlii.Ubigia.Infrastructure.Hosting.TestHost.AspNetCore;
//
//
//    public interface IHostTestContext<out TInfrastructureTestHost> : IHostTestContext
//        where TInfrastructureTestHost : class, IInfrastructureTestHost
//    {
//        TInfrastructureTestHost Host { get; }
//    }
//
//    public interface IHostTestContext
//    {
//        string SystemAccountName { get; }
//        string SystemAccountPassword { get; }
//        string TestAccountName { get; }
//        string TestAccountPassword { get; }
//        string AdminAccountName { get; }
//        string AdminAccountPassword { get; }
//
//        Uri HostAddress { get; }
//        Uri ManagementServiceAddress { get; }
//        Uri DataServiceAddress { get; }
//
//        string HostName { get; }
//
//        void Start(bool useRandomPorts = false);
//
//        //void Start(IHost host, IInfrastructure infrastructure);
//        void Stop();
//
//        Task<ISystemConnection> CreateSystemConnection();
//
//        Task AddUserAccountAndSpaces(ISystemConnection connection, string accountName, string password, string[] spaceNames);
//    }
//}