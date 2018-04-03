﻿namespace EtAlii.Ubigia.Infrastructure.Hosting
{
	using System;
	using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using EtAlii.Ubigia.Infrastructure.Transport;

    public interface IHostTestContext
    {
        InProcessInfrastructureTestHost Host { get; }
	    string SystemAccountName { get; }
	    string SystemAccountPassword { get; }
	    string TestAccountName { get; }
	    string TestAccountPassword { get; }
	    string AdminAccountName { get; }
	    string AdminAccountPassword { get; }

		Uri HostAddress { get; }
	    Uri ManagementServiceAddress { get; }
	    Uri DataServiceAddress { get; }

		string HostName { get; }

	    IInfrastructureClient CreateRestInfrastructureClient();

		void Start();

        //void Start(IHost host, IInfrastructure infrastructure);
        void Stop();

        Task<ISystemConnection> CreateSystemConnection();

        Task AddUserAccountAndSpaces(ISystemConnection connection, string accountName, string password, string[] spaceNames);
    }
}