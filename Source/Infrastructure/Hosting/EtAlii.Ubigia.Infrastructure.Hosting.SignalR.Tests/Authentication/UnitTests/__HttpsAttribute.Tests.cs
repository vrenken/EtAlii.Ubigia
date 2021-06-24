// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

//namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
//[
//    using Xunit
//    using System
//    using System.Collections.ObjectModel
//    using System.Net
//    using System.Net.Http
//    using System.Threading.Tasks
//    using System.Web.Http
//    using System.Web.Http.Controllers
//    using System.Web.Http.Routing
//    using EtAlii.Ubigia.Infrastructure.Transport.Owin.Rest

//    public class HttpsAttribute_Tests
//    [
//        [Fact]
//        public void HttpsAttribute_Create_Correct()
//        [
//            var attribute = new HttpsAttribute()

//            var actionContext = CreateContext("https://test")
//            attribute.OnActionExecuting(actionContext)

//            Assert.Null(actionContext.Response)
//        ]
//        [Fact]
//        public void HttpsAttribute_Create_Incorrect()
//        [
//            var attribute = new HttpsAttribute()

//            var actionContext = CreateContext("http://test")
//            attribute.OnActionExecuting(actionContext)

//            Assert.NotNull(actionContext.Response)
//            Assert.IsType< HttpResponseMessage>(actionContext.Response)
//            Assert.Equal(HttpStatusCode.BadRequest, actionContext.Response.StatusCode)
//        ]
//        private HttpActionContext CreateContext(string uri)
//        [
//            return new HttpActionContext
//            (
//                new HttpControllerContext
//                (
//                    new HttpConfiguration(),
//                    new HttpRouteData(new HttpRoute(null)),
//                    new HttpRequestMessage(HttpMethod.Get, uri)
//                ),
//                new ImplementedHttpActionDescriptor()
//            )
//        ]
//        public class ImplementedHttpActionDescriptor : HttpActionDescriptor
//        [
//            public override string ActionName => "ActionName"

//            public override System.Threading.Tasks.Task<object> ExecuteAsync(HttpControllerContext controllerContext, System.Collections.Generic.IDictionary<string, object> arguments, System.Threading.CancellationToken cancellationToken)
//            [
//                return Task.FromResult(new object())
//            ]
//            public override Collection<HttpParameterDescriptor> GetParameters()
//            [
//                return new Collection<HttpParameterDescriptor>()
//            ]
//            public override Type ReturnType => typeof(string)
//        ]
//    ]
//]
