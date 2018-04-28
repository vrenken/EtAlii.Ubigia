namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
{
	using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public interface IHttpContextResponseBuilder
	{
		IActionResult Build(HttpContext context, Controller controller, string accountName);
    }
}