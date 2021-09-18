// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.ComponentModel;
    using System.Threading;

    public class DockerDialog
    {
        private readonly IHost _host;
        private readonly ManualResetEventSlim _endEvent = new ();

        public DockerDialog(IHost host)
        {
            _host = host;
            _host.PropertyChanged += OnHostPropertyChanged;
        }

        private void OnHostPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(_host.State):
                    OnHostStateChanged(_host.State);
                    break;
                case nameof(_host.Commands):
                    WriteHeaderAndStatus();
                    break;
            }
        }

        private void OnHostStateChanged(State state)
        {
            switch (state)
            {
                case State.Shutdown:
                    Console.WriteLine("-----------------------------");
                    Console.WriteLine("Host has shut down. Have a nice day.");
                    _endEvent.Set();
                    break;
            }
            Console.WriteLine();
        }

        public void Start()
        {
            WriteHeaderAndStatus();

            _endEvent.Wait();
            Environment.Exit(0);
        }

        private void WriteHeaderAndStatus()
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine("[Host]");
            Console.WriteLine($"State: {_host.State}");
            Console.WriteLine();
            Console.WriteLine("-----------------------------");
            foreach (var status in _host.Status)
            {
                if (string.IsNullOrWhiteSpace(status.Title) || string.IsNullOrWhiteSpace(status.Summary)) continue;

                Console.WriteLine($"[{status.Title}]");
                Console.WriteLine(status.Summary.TrimEnd(Environment.NewLine.ToCharArray()));
                Console.WriteLine();
            }
        }
    }
}
