// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal
{
    using System;
    using System.IO;
    using System.Reflection;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.StaticWebAssets;

    internal static class WebHostEnvironmentUseStaticWebAssetsExtension
    {
        /// <summary>
        ///     Serve static web assets provided by the module.
        /// </summary>
        public static IWebHostEnvironment UseStaticWebAssets<TModule>(this IWebHostEnvironment environment)
        {
            var assembly = typeof(TModule).Assembly;
            var assemblyDirectory = Path.GetDirectoryName(assembly.Location)!;

            var manifestPath = Path.Combine(assemblyDirectory!, $"{assembly.GetName().Name}.StaticWebAssets.runtime.json");

            if (!File.Exists(manifestPath))
            {
                throw new InvalidOperationException("Unable to find StaticWebAssets.json");
            }

            using var manifest = File.OpenRead(manifestPath);

            var useStaticWebAssetsCoreMethod = typeof(StaticWebAssetsLoader).GetMethod("UseStaticWebAssetsCore", BindingFlags.NonPublic | BindingFlags.Static);

            useStaticWebAssetsCoreMethod!.Invoke(null, new object[] { environment, manifest });
            return environment;
        }
    }
}
