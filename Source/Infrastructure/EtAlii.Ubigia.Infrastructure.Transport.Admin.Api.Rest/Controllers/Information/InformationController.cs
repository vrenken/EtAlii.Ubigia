// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Rest;

using System;
using System.Linq;
using EtAlii.Ubigia.Api.Transport;
using EtAlii.Ubigia.Api.Transport.Rest;
using EtAlii.Ubigia.Infrastructure.Functional;
using EtAlii.Ubigia.Infrastructure.Transport.Rest;
using Microsoft.AspNetCore.Mvc;

[RequiresAuthenticationToken(Role.Admin)]
[Route(RelativeManagementUri.Information)]
public class InformationController : RestController
{
    private readonly ServiceDetails _serviceDetails;

    public InformationController(IFunctionalContext functionalContext)
    {
        _serviceDetails = functionalContext.Options.ServiceDetails.Single(sd => sd.Name == ServiceDetailsName.Rest);
    }

    [HttpGet]
    public IActionResult GetLocalConnectivityDetails([RequiredFromQuery]string connectivity)
    {
        IActionResult response;
        try
        {
            var details = new ConnectivityDetails
            {
                ManagementAddress = _serviceDetails.ManagementAddress.ToString(),
                DataAddress = _serviceDetails.DataAddress.ToString()
            };

            response = Ok(details);
        }
        catch (Exception ex)
        {
            //Logger.Critical("Unable to serve a Information GET client request", ex)
            response = BadRequest(ex.Message);
        }
        return response;
    }
}
