// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Service.Rest;

using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

public class TypedControllerFeatureProvider<TController> : ControllerFeatureProvider, ITypedControllerFeatureProvider
    where TController : ControllerBase
{
    protected override bool IsController(TypeInfo typeInfo)
    {
        return typeof(TController).GetTypeInfo().IsAssignableFrom(typeInfo) && base.IsController(typeInfo);
    }
}
