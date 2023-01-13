// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Service.Rest;

using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

public static class EndpointRouteBuilderDiagnosticsExtension
{
    public static IEndpointRouteBuilder AddDebugRouter(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("ShowRoutes", async context =>
        {
            var actionDescriptorCollectionProvider = context.RequestServices.GetService<IActionDescriptorCollectionProvider>();
            var routes = actionDescriptorCollectionProvider!.ActionDescriptors.Items.Select(x => new
            {
                Action = x.RouteValues["Action"],
                Controller = x.RouteValues["Controller"],
                x.AttributeRouteInfo?.Name,
                x.AttributeRouteInfo?.Template,
                Constraint = x.ActionConstraints
            }).ToList();

            var lines = routes.Select(r => $"{r.Template} => {r.Controller}.{r.Action} {Environment.NewLine}");
            var content = string.Concat(lines);
            await context.Response.WriteAsync(content).ConfigureAwait(false);
        });
        return endpoints;
    }
}
