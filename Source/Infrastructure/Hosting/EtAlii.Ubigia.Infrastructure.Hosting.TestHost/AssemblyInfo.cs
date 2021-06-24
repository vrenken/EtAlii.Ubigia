// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

// This code is only used for testing. It should be refactored the moment our tests will run on non-windows environments.
[assembly:SupportedOSPlatform("windows")]

[assembly: InternalsVisibleTo("EtAlii.Ubigia.Api.Functional.Context")]
[assembly: InternalsVisibleTo("EtAlii.Ubigia.Api.Functional.Context.SignalR.Tests")]
[assembly: InternalsVisibleTo("EtAlii.Ubigia.Api.Functional.Context.Grpc.Tests")]
[assembly: InternalsVisibleTo("EtAlii.Ubigia.Api.Functional.Context.Rest.Tests")]

[assembly: InternalsVisibleTo("EtAlii.Ubigia.Api.Functional.Traversal.Diagnostics")]
[assembly: InternalsVisibleTo("EtAlii.Ubigia.Api.Functional.SignalR.Tests")]
[assembly: InternalsVisibleTo("EtAlii.Ubigia.Api.Functional.Grpc.Tests")]
[assembly: InternalsVisibleTo("EtAlii.Ubigia.Api.Functional.Rest.Tests")]
