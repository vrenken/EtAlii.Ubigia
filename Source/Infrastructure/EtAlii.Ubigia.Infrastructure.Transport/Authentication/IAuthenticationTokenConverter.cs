// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    public interface IAuthenticationTokenConverter
    {
        byte[] ToBytes(AuthenticationToken token);
        AuthenticationToken FromBytes(byte[] tokenAsBytes);
        AuthenticationToken FromString(string authenticationTokenAsString);
        string ToString(AuthenticationToken token);

    }
}
