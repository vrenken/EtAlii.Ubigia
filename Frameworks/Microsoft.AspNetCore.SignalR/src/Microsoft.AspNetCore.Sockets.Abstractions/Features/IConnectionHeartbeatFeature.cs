// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// ReSharper disable All

using System;

namespace Microsoft.AspNetCore.Sockets.Features
{
    public interface IConnectionHeartbeatFeature
    {
        void OnHeartbeat(Action<object> action, object state);
    }
}
