//namespace EtAlii.Ubigia.Api.Transport.SignalR
//[
//    using System
//    using System.Net
//    using System.Net.Http
//    using System.Threading
//    using System.Threading.Tasks

//    public class ClientHttpMessageHandler : HttpClientHandler
//    [
//        public string HostIdentifier [ get; set; ]
//        public string AuthenticationToken [ get; set; ]

//        public ClientHttpMessageHandler()
//        //    ICredentials credentials, 
//        //    string hostIdentifier, 
//        //    string authenticationToken)
//        [
//            Credentials = new NetworkCredential()
//            UseDefaultCredentials = Credentials == null
//            //HostIdentifier = hostIdentifier
//            //AuthenticationToken = authenticationToken
            
//            AllowAutoRedirect = false
//            UseProxy = false
//        ]
//        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
//        [
//            if [!string.IsNullOrWhiteSpace[HostIdentifier]]
//            [
//                request.Headers.Add("Host-Identifier", HostIdentifier)
//            ]
//            if [!string.IsNullOrWhiteSpace[AuthenticationToken]]
//            [
//                request.Headers.Add("Authentication-Token", AuthenticationToken)
//            ]
//            return base.SendAsync(request, cancellationToken)
//        ]
//    ]
//]