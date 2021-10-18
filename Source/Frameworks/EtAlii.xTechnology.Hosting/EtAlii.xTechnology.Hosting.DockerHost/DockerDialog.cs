// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Threading;
    using Serilog;

    public class DockerDialog
    {
        private readonly IHost _host;
        private readonly ILogger _logger = Log.ForContext<DockerDialog>();
        private readonly CancellationTokenSource _cancellationTokenSource = new ();

        public DockerDialog(IHost host)
        {
            _host = host;
            _host.PropertyChanged += (_, _) =>
            {
                WriteHeaderAndStatus();
                if (_host.State == State.Shutdown)
                {
                    _cancellationTokenSource.Cancel();
                }
            };
        }

        public void Start()
        {
            WriteHeaderAndStatus();

            _cancellationTokenSource.Token.WaitHandle.WaitOne();

            _logger.Information("Host has shut down. Have a nice day");
            Environment.Exit(0);
        }

        private void WriteHeaderAndStatus()
        {
            foreach (var status in _host.Status)
            {
                _logger
                    .ForContext("StatusSummary", status.Summary ?? string.Empty)
                    .Information("{ServiceId} status", status.Id);
            }
        }
    }
}
