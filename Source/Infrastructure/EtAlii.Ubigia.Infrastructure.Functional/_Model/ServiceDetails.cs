// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;

    public class ServiceDetails
    {
        /// <summary>
        /// The name of the service to which the details relate.   
        /// </summary>
        public string Name { get; }
        
        public bool IsSystemService { get; }
        /// <summary>
        /// The address of the service management API.
        /// </summary>
        public Uri ManagementAddress { get; }

        /// <summary>
        /// The address of the service data API.
        /// </summary>
        public Uri DataAddress { get; }
        
        public ServiceDetails(string name, Uri managementAddress, Uri dataAddress, bool isSystemService)
        {
            Name = name;
            ManagementAddress = managementAddress;
            DataAddress = dataAddress;
            IsSystemService = isSystemService;
        }
    }
}