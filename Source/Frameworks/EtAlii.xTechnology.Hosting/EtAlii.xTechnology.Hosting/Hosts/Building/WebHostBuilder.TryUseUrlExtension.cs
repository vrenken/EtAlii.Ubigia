// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting;

using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

public static class WebHostBuilderTryUseUrlExtension
{
    public static bool TryUseUrl(this IWebHostBuilder builder, string schema, HostString hostString)
    {
        var ipAddress = !Equals(hostString.Host, IPAddress.None.ToString())
            ? IPAddress.Parse(hostString.Host)
            : IPAddress.Any;

        var port = hostString.Port ?? -1;
        if (port == -1)
        {
            return false;
        }
        var allUrlsAsString = builder.GetSetting(WebHostDefaults.ServerUrlsKey);
        var serviceUrl = $"{schema}://{ipAddress}:{port}";
        if (allUrlsAsString != null)
        {
            var allUrls = allUrlsAsString
                .Split(";")
                .ToList();
            if (allUrls.Any(url => url == serviceUrl))
            {
                return false;
            }

            allUrls.Add(serviceUrl);
            builder.UseUrls(allUrls.ToArray());
            return true;
        }

        builder.UseUrls(serviceUrl);
        return true;
    }
}
