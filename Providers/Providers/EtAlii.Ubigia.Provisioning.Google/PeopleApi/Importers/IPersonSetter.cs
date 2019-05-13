// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;
    using global::Google.Apis.PeopleService.v1.Data;

    public interface IPersonSetter
    {
        Task Set(IGraphSLScriptContext context, Person person);
    }
}