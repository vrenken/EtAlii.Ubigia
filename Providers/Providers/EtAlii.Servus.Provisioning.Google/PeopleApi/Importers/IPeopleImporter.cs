// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Servus.Provisioning.Google.PeopleApi
{
    using System;

    public interface IPeopleImporter : IImporter
    {
        event Action<Exception> Error;
    }
}