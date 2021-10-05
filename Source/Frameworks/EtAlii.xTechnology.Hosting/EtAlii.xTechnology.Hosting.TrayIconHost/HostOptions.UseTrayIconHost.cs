// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Windows;
    using EtAlii.xTechnology.MicroContainer;

    public static class HostOptionsUseTrayIconHostExtension
    {
        public static HostOptions UseTrayIconHost(
            this HostOptions options,
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

            return options
                .Use(new IExtension[] { new TrayIconHostExtension(options, runningIcon, stoppedIcon, errorIcon) })
                .UseWrapper(true);
        }

        private static Icon ToIcon(Assembly assembly, string resource)
        {
            var resourceNamespace = Path.GetFileNameWithoutExtension(assembly.Location);

            using var stream = assembly.GetManifestResourceStream($"{resourceNamespace}.{resource}");
            if (stream == null)
            {
                throw new InvalidOperationException($"Unable to get manifest resource stream for resource: {resource}");
            }
            return new Icon(stream);
        }
    }
}
