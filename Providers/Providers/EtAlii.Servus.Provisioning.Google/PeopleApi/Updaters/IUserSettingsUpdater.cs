// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Servus.Provisioning.Google.PeopleApi
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Functional;

    public interface IUserSettingsUpdater
    {
        Task Update(UserSettings userSettings, SystemSettings systemSettings, IDataContext userDataContext, TimeSpan thresholdBeforeExpiration);
    }
}