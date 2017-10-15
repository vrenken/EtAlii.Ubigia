namespace EtAlii.Servus.Infrastructure.SignalR
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Configuration;
    using Microsoft.AspNet.SignalR.Hubs;
    using Microsoft.AspNet.SignalR.Infrastructure;
    using Microsoft.AspNet.SignalR.Messaging;
    using Microsoft.AspNet.SignalR.Tracing;
    using Microsoft.AspNet.SignalR.Transports;
    using Newtonsoft.Json;

    public class CustomDefaultDependencyResolver : DefaultDependencyResolver
    {
        public CustomDefaultDependencyResolver()
        {
            // Let's use the latest NewtonSoft JSON serializer version - not doing so causes errors on the build server for some weird reason.
            var serializer = new Lazy<JsonSerializer>();
            this.Register(typeof(JsonSerializer), () => serializer.Value);
        }
    }
}