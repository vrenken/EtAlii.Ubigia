// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Rest
{
    using System.Net.Http.Headers;

    public static class MediaType
    {
        public static readonly MediaTypeWithQualityHeaderValue Bson = new("application/bson");
    }
}
