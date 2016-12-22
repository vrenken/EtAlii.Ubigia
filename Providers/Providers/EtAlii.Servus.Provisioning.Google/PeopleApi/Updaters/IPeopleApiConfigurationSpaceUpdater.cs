// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Servus.Provisioning.Google.PeopleApi
{
    public interface IPeopleApiConfigurationSpaceUpdater
    {
        void Update(ConfigurationSpace configurationSpace, SystemSettings systemSettings);
    }
}