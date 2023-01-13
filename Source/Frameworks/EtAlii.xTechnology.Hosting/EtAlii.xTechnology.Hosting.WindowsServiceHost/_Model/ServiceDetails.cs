// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting;

public class ServiceDetails
{
    public string Name { get;  }
    public string DisplayName { get;  }
    public string Description { get;  }

    public ServiceDetails(
        string configurationName, // e.g. _host.Configuration.Name
        string shortServiceNameFormat, // Something like: "UIS${0}" Ubigia Infrastructure Service
        string displayNameFormat, // example: "Ubigia Infrastructure Service ({0})"
        string descriptionFormat // e.g. "Provides applications access to the Ubigia storage '{0}'"
    )
    {
        Name = string.Format(shortServiceNameFormat, configurationName).Replace(" ", "_");
        DisplayName = string.Format(displayNameFormat, configurationName);
        Description = string.Format(descriptionFormat, configurationName);
    }
}
