﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// ReSharper disable All

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Sockets.Http.Features
{
    public interface IHttpContextFeature
    {
        HttpContext HttpContext { get; set; }
    }

    public class HttpContextFeature : IHttpContextFeature
    {
        public HttpContext HttpContext { get; set; }
    }
}
