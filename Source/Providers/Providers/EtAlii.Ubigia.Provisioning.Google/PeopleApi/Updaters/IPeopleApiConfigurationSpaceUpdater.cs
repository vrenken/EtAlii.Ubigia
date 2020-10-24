// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System.Threading.Tasks;

    public interface IPeopleApiConfigurationSpaceUpdater
    {
        Task Update(ConfigurationSpace configurationSpace, SystemSettings systemSettings);
    }
}