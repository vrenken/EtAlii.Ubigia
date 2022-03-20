// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal
{
    using System;
    using System.IO;
    using System.Reflection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.StaticWebAssets;
    using Microsoft.AspNetCore.StaticFiles;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.Options;
    using Serilog;

    public class UIConfigureOptions : IPostConfigureOptions<StaticFileOptions>
    {
        private readonly IWebHostEnvironment _environment;

        public UIConfigureOptions(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public void PostConfigure(string name, StaticFileOptions options)
        {
            name = name ?? throw new ArgumentNullException(nameof(name));
            options = options ?? throw new ArgumentNullException(nameof(options));

            // Basic initialization in case the options weren't initialized by any other component
            options.ContentTypeProvider ??= new FileExtensionContentTypeProvider();
            if (options.FileProvider == null && _environment.WebRootFileProvider == null)
            {
                throw new InvalidOperationException("Missing FileProvider");
            }

            options.FileProvider ??= _environment.WebRootFileProvider;

            var basePath = "wwwroot";

            var filesProvider = new ManifestEmbeddedFileProvider(GetType().Assembly, basePath);
            options.FileProvider = new CompositeFileProvider(options.FileProvider, filesProvider);
        }
    }

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

            var log = Log.ForContext("SourceContext", nameof(WebHostEnvironmentUseStaticWebAssetsExtension));

            var files = Directory.GetFiles(assemblyDirectory);
            foreach (var file in files)
            {
                log.Information("Found file: {FileName}", file);
            }

            if (!File.Exists(manifestPath))
            {
                throw new InvalidOperationException($"Unable to find StaticWebAssets.json at: {manifestPath}");
            }

            using var manifest = File.OpenRead(manifestPath);

            #pragma warning disable S3011 // Reflection should not be used to increase accessibility of classes, methods, or fields
            var useStaticWebAssetsCoreMethod = typeof(StaticWebAssetsLoader).GetMethod("UseStaticWebAssetsCore", BindingFlags.NonPublic | BindingFlags.Static);
            #pragma warning restore S3011

            useStaticWebAssetsCoreMethod!.Invoke(null, new object[] { environment, manifest });
            return environment;
        }
    }
}
