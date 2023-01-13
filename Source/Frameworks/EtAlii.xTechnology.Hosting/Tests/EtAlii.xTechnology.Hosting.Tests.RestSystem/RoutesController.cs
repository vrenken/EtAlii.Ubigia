// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.RestSystem;

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

[Route("routes")]
public class RoutesController : Controller
{
    private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

    public RoutesController(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
    {
        _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
    }

    [HttpGet]
    [HttpPut]
    public IActionResult Index()
    {
        var routes = _actionDescriptorCollectionProvider.ActionDescriptors.Items.Select(x => new {
            Action = x.RouteValues["Action"],
            Controller = x.RouteValues["Controller"],
            x.AttributeRouteInfo?.Name,
            x.AttributeRouteInfo?.Template,
            Contraint = x.ActionConstraints
        }).ToList();

        return Ok(routes);
    }
}
