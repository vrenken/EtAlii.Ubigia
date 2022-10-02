// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest

{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Newtonsoft.Json;

    [Route("routes")]
    public class RoutesController : RestController
    {
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
        private readonly JsonSerializerSettings _settings;

        public RoutesController(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
            _settings = new JsonSerializerSettings {Formatting = Formatting.Indented};
        }

        [HttpGet]
        [HttpPut]
        public IActionResult Index()
        {
            var routes = _actionDescriptorCollectionProvider.ActionDescriptors.Items.Select(x => new {
                Controller = x.RouteValues["Controller"],
                Action = x.RouteValues["Action"],
                x.AttributeRouteInfo?.Name,
                x.AttributeRouteInfo?.Template,
                Contraint = x.ActionConstraints
            }).ToList();

            return Json(routes, _settings);
        }
    }
}
