namespace EtAlii.Ubigia.Infrastructure.Transport.Setup.Portal;

using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[SuppressMessage(
    category: "Sonar Code Smell",
    checkId: "S4502:Disabling CSRF protections is security-sensitive",
    Justification = "Default pattern as specified in the Asp.NET Core Razor/Blazor approach. So probably safe to do so here.")]
[IgnoreAntiforgeryToken]
public class ErrorModel : PageModel
{
    public string RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
