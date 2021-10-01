// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Rest
{
	using System;
	using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Rest;
    using EtAlii.Ubigia.Infrastructure.Transport.Rest;
	using EtAlii.xTechnology.Hosting;
	using Microsoft.AspNetCore.Mvc;

	[RequiresAuthenticationToken(Role.Admin)]
    [Route(RelativeManagementUri.Information)]
    public class InformationController : RestController
    {
	    private readonly IConfigurationDetails _configurationDetails;

	    public InformationController(IConfigurationDetails configurationDetails)
	    {
		    _configurationDetails = configurationDetails;
	    }

	    [HttpGet]
	    public IActionResult GetLocalConnectivityDetails([RequiredFromQuery]string connectivity)
	    {
		    IActionResult response;
		    try
            {
                if (!_configurationDetails.Paths.TryGetValue("AdminApiPathRest", out var adminApiPathRest))
                {
                    adminApiPathRest = string.Empty;
                }

                if(!_configurationDetails.Paths.TryGetValue("UserApiPathRest", out var userApiPathRest))
                {
                    userApiPathRest = string.Empty;
                }

			    var details = new ConnectivityDetails
			    {
				    ManagementAddress = $"https://{_configurationDetails.Hosts["AdminHost"]}:{_configurationDetails.Ports["AdminApiPort"]}{_configurationDetails.Paths["AdminApiPath"]}{adminApiPathRest}",
				    DataAddress = $"https://{_configurationDetails.Hosts["UserHost"]}:{_configurationDetails.Ports["UserApiPort"]}{_configurationDetails.Paths["UserApiPath"]}{userApiPathRest}",
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
}
