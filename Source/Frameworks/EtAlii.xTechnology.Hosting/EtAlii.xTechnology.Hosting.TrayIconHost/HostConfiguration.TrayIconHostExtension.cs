// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Windows;

    public static class HostConfigurationTrayIconHostExtension
    {
        public static IHostConfiguration UseTrayIconHost(
            this IHostConfiguration configuration,
            Application application,
            string runningIconResource,
            string stoppedIconResource,
            string errorIconResource)
        {
            application.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            var assembly = application.GetType().Assembly;
            var runningIcon = ToIcon(assembly, runningIconResource);
            var stoppedIcon = ToIcon(assembly, stoppedIconResource);
            var errorIcon = ToIcon(assembly, errorIconResource);

            var extensions = new IHostExtension[]
            {
                new TrayIconHostExtension(runningIcon, stoppedIcon, errorIcon),
            };
            return configuration.Use(extensions);
        }

        private static Icon ToIcon(Assembly assembly, string resource)
        {
            var resourceNamespace = Path.GetFileNameWithoutExtension(assembly.Location);
            using (var stream = assembly.GetManifestResourceStream($"{resourceNamespace}.{resource}"))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException($"Unable to get manifest resource stream for resource: {resource}");
                }
                return new Icon(stream);
            }
        }
    }
}