//namespace EtAlii.Ubigia.Infrastructure.Transport.Grpc
//[
//    using Microsoft.Grpc.Mvc
//    using Microsoft.Grpc.Mvc.Filters

//    internal class HttpsAttribute : ActionFilterAttribute
//    [
//        public override void OnActionExecuting(ActionExecutingContext actionContext)
//        [
//            if (!actionContext.HttpContext.Request.IsHttps)
//            [
//                actionContext.Result = new BadRequestResult()
//            }
//        }
//    }
//}