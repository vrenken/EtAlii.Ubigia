//namespace EtAlii.Servus.Infrastructure.WebApi
//{
//    using System.Web.Http;
//    using Microsoft.AspNet.SignalR;
//    using Owin;

//    public partial class ApiComponent : IApiComponent
//    {
//        private readonly DefaultDependencyResolver _signalRDependencyResolver;
//        private readonly HttpConfiguration _httpConfiguration;

//        public ApiComponent(
//            DefaultDependencyResolver signalRDependencyResolver, 
//            HttpConfiguration httpConfiguration)
//        {
//            _signalRDependencyResolver = signalRDependencyResolver;
//            _httpConfiguration = httpConfiguration;
//        }

//        public void Start(IAppBuilder application)
//        {
//            StartSignalRServices(application);
//            StartWebApiServices(application);
//        }

//        public void Stop()
//        {
//            //throw new System.NotImplementedException();
//        }
//    }
//}