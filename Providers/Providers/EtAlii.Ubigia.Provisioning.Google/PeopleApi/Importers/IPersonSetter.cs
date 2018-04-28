// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using EtAlii.Ubigia.Api.Functional;
    using global::Google.Apis.PeopleService.v1.Data;

    public interface IPersonSetter
    {
        void Set(IDataContext context, Person person);
    }
}