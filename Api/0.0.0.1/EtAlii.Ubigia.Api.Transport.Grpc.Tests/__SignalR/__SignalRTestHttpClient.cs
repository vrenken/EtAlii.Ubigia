//namespace EtAlii.Ubigia.Api.Transport.Tests
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Net.Http;
//    using System.Threading;
//    using System.Threading.Tasks;
//    using EtAlii.Ubigia.Api.Transport.SignalR.Tests;
//    using Microsoft.AspNetCore.SignalR.Client;
//    using Microsoft.AspNetCore.SignalR.Client.Internal.Http;

//    public class SignalRTestHttpClient : IHttpClient
//    {
//        private readonly Func<IConnection, HttpMessageHandler> _createMessageHandler;
//        private HttpClient _longRunningClient;
//        private HttpClient _shortRunningClient;
//        private IConnection _connection;

//        public SignalRTestHttpClient(Func<IConnection, HttpMessageHandler> createMessageHandler)
//        {
//            _createMessageHandler = createMessageHandler;
//        }

//        /// <summary>
//        /// Initialize the Http Clients
//        /// 
//        /// </summary>
//        /// <param name="connection">Connection</param>
//        public void Initialize(IConnection connection)
//        {
//            _connection = connection;
//            _longRunningClient = new HttpClient(_createMessageHandler(_connection))
//            {
//                Timeout = TimeSpan.FromMilliseconds(-1.0)
//            };
//            _shortRunningClient = new HttpClient(_createMessageHandler(_connection))
//            {
//                Timeout = TimeSpan.FromMilliseconds(-1.0)
//            };
//        }

//        /// <summary>
//        /// Makes an asynchronous http GET request to the specified url.
//        /// 
//        /// </summary>
//        /// <param name="url">The url to send the request to.</param><param name="prepareRequest">A callback that initializes the request with default values.</param><param name="isLongRunning">Indicates whether the request is long running</param>
//        /// <returns>
//        /// A <see cref="T:Task{IResponse}"/>.
//        /// </returns>
//        public Task<IResponse> Get(string url, Action<IRequest> prepareRequest, bool isLongRunning)
//        {
//            if (prepareRequest == null)
//            {
//                throw new ArgumentNullException("prepareRequest");
//            }
//            var responseDisposer = new SignalRDisposer();
//            var cts = new CancellationTokenSource();
//            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri(url));
//            var requestMessageWrapper = new HttpRequestMessageWrapper(httpRequestMessage, () =>
//            {
//                cts.Cancel();
//                responseDisposer.Dispose();
//            });
//            prepareRequest((IRequest)requestMessageWrapper);
//            return SignalRTaskAsyncHelper.Then(GetHttpClient(isLongRunning).SendAsync(httpRequestMessage, HttpCompletionOption.ResponseHeadersRead, cts.Token), (Func<HttpResponseMessage, IResponse>)(responseMessage =>
//            {
//                if (!responseMessage.IsSuccessStatusCode)
//                {
//                    throw new HttpClientException(responseMessage);
//                }
//                responseDisposer.Set((IDisposable)responseMessage);
//                return (IResponse)new HttpResponseMessageWrapper(responseMessage);
//            }));
//        }

//        /// <summary>
//        /// Makes an asynchronous http POST request to the specified url.
//        /// 
//        /// </summary>
//        /// <param name="url">The url to send the request to.</param><param name="prepareRequest">A callback that initializes the request with default values.</param><param name="postData">form url encoded data.</param><param name="isLongRunning">Indicates whether the request is long running</param>
//        /// <returns>
//        /// A <see cref="T:Task{IResponse}"/>.
//        /// </returns>
//        public Task<IResponse> Post(string url, Action<IRequest> prepareRequest, IDictionary<string, string> postData, bool isLongRunning)
//        {
//            if (prepareRequest == null)
//            {
//                throw new ArgumentNullException("prepareRequest");
//            }
//            var responseDisposer = new SignalRDisposer();
//            var cts = new CancellationTokenSource();
//            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri(url))
//            {
//                Content = postData != null
//                        ? new SignalRFormUrlEncodedContent(postData)
//                        : (HttpContent) new StringContent(string.Empty)
//            };
//            var requestMessageWrapper = new HttpRequestMessageWrapper(httpRequestMessage, () =>
//            {
//                cts.Cancel();
//                responseDisposer.Dispose();
//            });
//            prepareRequest((IRequest)requestMessageWrapper);
//            return SignalRTaskAsyncHelper.Then(GetHttpClient(isLongRunning).SendAsync(httpRequestMessage, HttpCompletionOption.ResponseHeadersRead, cts.Token), (Func<HttpResponseMessage, IResponse>)(responseMessage =>
//            {
//                if (!responseMessage.IsSuccessStatusCode)
//                {
//                    throw new HttpClientException(responseMessage);
//                }
//                responseDisposer.Set((IDisposable)responseMessage);
//                return (IResponse)new HttpResponseMessageWrapper(responseMessage);
//            }));
//        }

//        /// <summary>
//        /// Returns the appropriate client based on whether it is a long running request
//        /// 
//        /// </summary>
//        /// <param name="isLongRunning">Indicates whether the request is long running</param>
//        /// <returns/>
//        private HttpClient GetHttpClient(bool isLongRunning)
//        {
//            if (!isLongRunning)
//            {
//                return _shortRunningClient;
//            }
//            return _longRunningClient;
//        }
//    }
//}
