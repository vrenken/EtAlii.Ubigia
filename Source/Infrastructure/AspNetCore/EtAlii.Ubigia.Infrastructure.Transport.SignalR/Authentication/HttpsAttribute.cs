namespace EtAlii.Ubigia.Infrastructure.Transport.SignalR
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    internal class HttpsAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.IsHttps)
            {
                context.Result = new BadRequestResult();
            }
        }
    }
}