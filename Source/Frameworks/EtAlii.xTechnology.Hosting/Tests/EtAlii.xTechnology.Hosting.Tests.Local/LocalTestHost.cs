// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Local
{
	using EtAlii.xTechnology.Hosting;

	public class LocalTestHost : TestHost
    {
		public LocalTestHost(IHostOptions options)
		    : base(options, new System1HostServicesFactory())
	    {
		}
    }
}
