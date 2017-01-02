namespace EtAlii.Ubigia.Infrastructure.WebApi
{
    using System;
    using System.Net.Http.Headers;
    using System.Web.Http.Filters;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CacheWebApiAttribute : ActionFilterAttribute
    {
        public int Duration { get; set; }

        public CacheWebApiAttribute(int duration) : base()
        {
            Duration = duration;
        }

        public override void OnActionExecuted(HttpActionExecutedContext filterContext)
        {
            if (filterContext.Response != null)
            {
                filterContext.Response.Headers.CacheControl = new CacheControlHeaderValue()
                {
                    MaxAge = TimeSpan.FromMinutes(Duration),
                    MustRevalidate = true,
                    Private = true
                };
            }
        }
    }

}