namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
{
    using System.Net;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public interface IHttpContextResponseBuilder
	{
		IActionResult Build(HttpContext context, Controller controller, string accountName);
    }
}