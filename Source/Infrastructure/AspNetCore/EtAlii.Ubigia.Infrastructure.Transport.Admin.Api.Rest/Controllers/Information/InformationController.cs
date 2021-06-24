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
			    var details = new ConnectivityDetails
			    {
				    ManagementAddress = $"http://{_configurationDetails.Hosts["AdminHost"]}:{_configurationDetails.Ports["AdminPort"]}{_configurationDetails.Paths["AdminApi"]}{_configurationDetails.Paths["AdminApiRest"]}",
				    DataAddress = $"http://{_configurationDetails.Hosts["UserHost"]}:{_configurationDetails.Ports["UserPort"]}{_configurationDetails.Paths["UserApi"]}{_configurationDetails.Paths["UserApiRest"]}",
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
