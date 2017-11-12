namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Collections.Generic;

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
            Name = String.Format(shortServiceNameFormat, configurationName).Replace(" ", "_");
            DisplayName = String.Format(displayNameFormat, configurationName);
            Description = String.Format(descriptionFormat, configurationName);
        }
    }

}
