﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport;

using Microsoft.AspNetCore.Http;

internal interface IHttpContextAuthenticationIdentityProvider
{
    AuthenticationIdentity Get(HttpContext context);
}
