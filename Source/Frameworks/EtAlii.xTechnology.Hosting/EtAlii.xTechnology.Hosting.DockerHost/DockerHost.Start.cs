// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class DockerHost
    {
        public static void Start(IHostOptions options)
        {
            var arguments = Environment.GetCommandLineArgs();
            for (var i = 0; i < arguments.Length; i++)
            {
                if (arguments[i] == "-d" && i + 1 < arguments.Length)
                {
                    var delay = int.Parse(arguments[i + 1]);
                    System.Threading.Tasks.Task.Delay(delay).Wait();
                }
            }

            Console.WriteLine("Starting Ubigia infrastructure...");

            var host = new HostFactory<DockerHost>().Create(options);

            // Start hosting both the infrastructure and the storage.
            host.Start();

            var consoleDialog = new DockerDialog(host);
            consoleDialog.Start();
        }
    }
}
