// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;

    public interface IUserSettingsUpdater
    {
        Task Update(UserSettings userSettings, SystemSettings systemSettings, IGraphSLScriptContext userDataContext, TimeSpan thresholdBeforeExpiration);
    }
}