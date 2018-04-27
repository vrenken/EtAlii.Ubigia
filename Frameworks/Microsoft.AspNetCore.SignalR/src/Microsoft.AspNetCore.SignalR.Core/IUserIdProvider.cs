﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// ReSharper disable All

namespace Microsoft.AspNetCore.SignalR.Core
{
    public interface IUserIdProvider
    {
        string GetUserId(HubConnectionContext connection);
    }
}