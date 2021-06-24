// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;

    internal class ResponseFeature : IHttpResponseFeature
    {
        private Func<Task> _responseStartingAsync = () => Task.FromResult(true);
        private Func<Task> _responseCompletedAsync = () => Task.FromResult(true);
        private readonly HeaderDictionary _headers = new();
        private int _statusCode;
        private string _reasonPhrase;

        public ResponseFeature()
        {
            Headers = _headers;
            Body = new MemoryStream();

            // 200 is the default status code all the way down to the host, so we set it
            // here to be consistent with the rest of the hosts when writing tests.
            StatusCode = 200;
        }

        public int StatusCode
        {
            get => _statusCode;
            set
            {
                if (HasStarted)
                {
                    throw new InvalidOperationException("The status code cannot be set, the response has already started.");
                }
                if (value < 100)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The status code cannot be set to a value less than 100");
                }

                _statusCode = value;
            }
        }

        public string ReasonPhrase
        {
            get => _reasonPhrase;
            set
            {
                if (HasStarted)
                {
                    throw new InvalidOperationException("The reason phrase cannot be set, the response has already started.");
                }

                _reasonPhrase = value;
            }
        }

        public IHeaderDictionary Headers { get; set; }

        public Stream Body { get; set; }

        public bool HasStarted { get; set; }

        public void OnStarting(Func<object, Task> callback, object state)
        {
            if (HasStarted)
            {
                throw new InvalidOperationException();
            }

            var prior = _responseStartingAsync;
            _responseStartingAsync = async () =>
            {
                await callback(state).ConfigureAwait(false);
                await prior().ConfigureAwait(false);
            };
        }

        public void OnCompleted(Func<object, Task> callback, object state)
        {
            var prior = _responseCompletedAsync;
            _responseCompletedAsync = async () =>
            {
                try
                {
                    await callback(state).ConfigureAwait(false);
                }
                finally
                {
                    await prior().ConfigureAwait(false);
                }
            };
        }

        public async Task FireOnSendingHeadersAsync()
        {
            await _responseStartingAsync().ConfigureAwait(false);
            HasStarted = true;
            _headers.IsReadOnly = true;
        }

        public Task FireOnResponseCompletedAsync()
        {
            return _responseCompletedAsync();
        }
    }
}
