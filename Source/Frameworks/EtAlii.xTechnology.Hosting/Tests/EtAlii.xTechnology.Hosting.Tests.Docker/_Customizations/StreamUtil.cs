// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace Docker.DotNet.Models
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    internal static class StreamUtil
    {
        private static readonly JsonSerializer _jsonSerializer = new();
        
        internal static async Task MonitorStreamAsync(Task<Stream> streamTask, DockerClient client, CancellationToken cancel, IProgress<string> progress)
        {
            await using var stream = await streamTask.ConfigureAwait(false);
            // ReadLineAsync must be cancelled by closing the whole stream.
            // ReSharper disable once AccessToDisposedClosure
            await using (cancel.Register(() => stream.Dispose()))
            {
                using (var reader = new StreamReader(stream, new UTF8Encoding(false)))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null)
                    {
                        progress.Report(line);
                    }
                }
            }
        }

        internal static async Task MonitorStreamForMessagesAsync<T>(Task<Stream> streamTask, DockerClient client, CancellationToken cancel, IProgress<T> progress)
        {
            await using var stream = await streamTask.ConfigureAwait(false);
            // ReadLineAsync must be cancelled by closing the whole stream.
            await using (cancel.Register(() => stream.Dispose()))
            {
                using (var reader = new StreamReader(stream, new UTF8Encoding(false)))
                {
                    try
                    {
                        string line;
                        while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null)
                        {
                            var prog = _jsonSerializer.DeserializeObject<T>(line);
                            if (prog == null) continue;

                            progress.Report(prog);
                        }
                    }
                    catch (ObjectDisposedException)
                    {
                        // The subsequent call to reader.ReadLineAsync() after cancellation
                        // will fail because we disposed the stream. Just ignore here.
                    }
                }
            }
        }

        internal static async Task MonitorResponseForMessagesAsync<T>(Task<HttpResponseMessage> responseTask, DockerClient client, CancellationToken cancellationToken, IProgress<T> progress)
        {
            using var response = await responseTask.ConfigureAwait(false);
            //await client.HandleIfErrorResponseAsync(response.StatusCode, response).ConfigureAwait(false);

            await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
            // ReadLineAsync must be cancelled by closing the whole stream.
            // ReSharper disable once AccessToDisposedClosure
            await using (cancellationToken.Register(() => stream.Dispose()))
            {
                using (var reader = new StreamReader(stream, new UTF8Encoding(false)))
                {
                    try
                    {
                        string line;
                        while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null)
                        {
                            var prog = _jsonSerializer.DeserializeObject<T>(line);
                            if (prog == null) continue;

                            progress.Report(prog);
                        }
                    }
                    catch (ObjectDisposedException)
                    {
                        // The subsequent call to reader.ReadLineAsync() after cancellation
                        // will fail because we disposed the stream. Just ignore here.
                    }
                }
            }
        }
    }
}