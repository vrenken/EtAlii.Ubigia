// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using EtAlii.Ubigia.Api.Functional;
    using global::Google.Contacts;

    public interface IPersonSetter
    {
        void Set(IDataContext context, Contact person);
    }
}