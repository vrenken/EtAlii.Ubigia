﻿//namespace RouteDebugging.Pages
//[
//    using System.Collections.Generic
//    using Newtonsoft.Json

//    public class RoutesModel : PageModel
//    [
//        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider

//        public RoutesModel(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
//        [
//            this._actionDescriptorCollectionProvider = actionDescriptorCollectionProvider
//        ]
//        public List<RouteInfo> Routes { get; set; }

//        public void OnGet()
//        [
//            Routes = _actionDescriptorCollectionProvider.ActionDescriptors.Items
//                .Select(x => new RouteInfo
//                [
//                    Action = x.RouteValues["Action"],
//                    Controller = x.RouteValues["Controller"],
//                    Name = x.AttributeRouteInfo.Name,
//                    Template = x.AttributeRouteInfo.Template,
//                    Constraint = x.ActionConstraints == null ? "" : JsonConvert.SerializeObject(x.ActionConstraints)
//                })
//                .OrderBy(r => r.Template)
//                .ToList()
//        ]
//        public class RouteInfo
//        [
//            public string Template { get; set; }
//            public string Name { get; set; }
//            public string Controller { get; set; }
//            public string Action { get; set; }
//            public string Constraint { get; set; }
//        ]
//    ]
//]