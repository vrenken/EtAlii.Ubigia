// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;

    public class PathStringBuilder
    {
        public PathString Build(IConfigurationSection configuration, IModule parentModule)
        {
            var path = configuration.GetValue("Path", string.Empty);
            var pathString = new PathString(path);
            if (parentModule != null)
            {
                pathString = parentModule.PathString.Add(pathString);
            }

            return pathString;
        }
    }
}
