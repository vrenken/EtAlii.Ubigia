// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.IO;
    using System.Text;
    using Microsoft.Extensions.Configuration;

    public static class NetworkDetailsConfigurationExtension
    {
        public static IConfigurationBuilder AddConfigurationDetails(this IConfigurationBuilder builder, ConfigurationDetails details)
        {
#pragma warning disable CA2000             
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(details.Configuration));
            return builder.AddJsonStream(stream);
#pragma warning restore CA2000             
        }
    }
}