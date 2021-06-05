using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

[assembly: InternalsVisibleTo("EtAlii.Ubigia.Infrastructure.Hosting.NetCore.Tests")]

// This code is only used for testing. It should be refactored the moment our tests will run on non-windows environments.
[assembly:SupportedOSPlatform("windows")]