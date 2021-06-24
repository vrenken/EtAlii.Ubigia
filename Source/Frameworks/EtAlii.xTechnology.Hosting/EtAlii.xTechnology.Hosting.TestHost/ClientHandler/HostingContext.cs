// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Diagnostics;
    using Microsoft.AspNetCore.Http;

    public struct HostingContext
    {
        public HttpContext HttpContext { get; set; }
        public IDisposable Scope { get; set; }
        public long StartTimestamp { get; set; }
        public bool EventLogEnabled { get; set; }
        public Activity Activity { get; set; }
    }
}
