﻿namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal.NetCore
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Newtonsoft.Json;

    [Route("routes")]
    public class RoutesController : AdminPortalController
    {
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
        private readonly JsonSerializerSettings _settings;

        public RoutesController(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
            _settings = new JsonSerializerSettings();
            _settings.Formatting = Formatting.Indented;
        }

        [HttpGet]
        [HttpPut]
        public IActionResult Index()
        {
            var routes = _actionDescriptorCollectionProvider.ActionDescriptors.Items.Select(x => new {
                Controller = x.RouteValues["Controller"],
                Action = x.RouteValues["Action"],
                Name = x.AttributeRouteInfo?.Name,
                Template = x.AttributeRouteInfo?.Template,
                Contraint = x.ActionConstraints
            }).ToList();

            return Json(routes, _settings);
        }
    }
}
