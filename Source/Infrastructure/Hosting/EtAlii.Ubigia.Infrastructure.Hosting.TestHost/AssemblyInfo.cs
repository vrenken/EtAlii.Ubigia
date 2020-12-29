﻿using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

// This code is only used for testing. It should be refactored the moment our tests will run on non-windows environments.
[assembly:SupportedOSPlatform("windows")]

[assembly: InternalsVisibleTo("EtAlii.Ubigia.Api.Functional.Querying")]

[assembly: InternalsVisibleTo("EtAlii.Ubigia.Api.Functional.Context")]
[assembly: InternalsVisibleTo("EtAlii.Ubigia.Api.Functional.Context.SignalR.Tests")]
[assembly: InternalsVisibleTo("EtAlii.Ubigia.Api.Functional.Context.Grpc.Tests")]
[assembly: InternalsVisibleTo("EtAlii.Ubigia.Api.Functional.Context.WebApi.Tests")]

[assembly: InternalsVisibleTo("EtAlii.Ubigia.Api.Functional.Querying.GraphQL")]
[assembly: InternalsVisibleTo("EtAlii.Ubigia.Api.Functional.Querying.GraphQL.SignalR.Tests")]
[assembly: InternalsVisibleTo("EtAlii.Ubigia.Api.Functional.Querying.GraphQL.Grpc.Tests")]
[assembly: InternalsVisibleTo("EtAlii.Ubigia.Api.Functional.Querying.GraphQL.WebApi.Tests")]

[assembly: InternalsVisibleTo("EtAlii.Ubigia.Api.Functional.Querying.Linq")]
[assembly: InternalsVisibleTo("EtAlii.Ubigia.Api.Functional.Querying.Linq.SignalR.Tests")]
[assembly: InternalsVisibleTo("EtAlii.Ubigia.Api.Functional.Querying.Linq.Grpc.Tests")]
[assembly: InternalsVisibleTo("EtAlii.Ubigia.Api.Functional.Querying.Linq.WebApi.Tests")]

[assembly: InternalsVisibleTo("EtAlii.Ubigia.Api.Functional.Traversal.Diagnostics")]
[assembly: InternalsVisibleTo("EtAlii.Ubigia.Api.Functional.Traversal.SignalR.Tests")]
[assembly: InternalsVisibleTo("EtAlii.Ubigia.Api.Functional.Traversal.Grpc.Tests")]
[assembly: InternalsVisibleTo("EtAlii.Ubigia.Api.Functional.Traversal.WebApi.Tests")]
